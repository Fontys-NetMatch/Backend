using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Infrastructure.Extensions;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.API.Response.Success.Product;
using TravelPlanner.API.Response.Success.ProductDate;
using TravelPlanner.API.Response.Success.ProductTranslation;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Request.Product;

namespace TravelPlanner.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductContainer _container;

        public ProductController(IProductContainer container)
        {
            _container = container;
        }

        public static void Register(WebApplication app)
        {

            // GetProduct by Id endpoint
            app.MapGet("/product/{productId:int}", (
                    [FromRoute] int productId,
                    [FromServices] ProductController controller
                ) => controller.GetProduct(productId))
                .WithName("GetProductById")
                .WithDescription("GetProduct a product by Id")
                .Produces<ProductResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(4)
                .WithOpenApi();

            // GetProduct all products endpoint
            app.MapGet("/product", (
                    [FromQuery] bool? isDeleted,
                    [FromQuery] int? typeId,
                    [FromQuery] string? searchQuery,
                    [FromQuery] string? startLocation,
                    [FromQuery] string? endLocation,
                    [FromQuery] DateTime? startDateTime,
                    [FromQuery] DateTime? endDateTime,
                    [FromQuery] int? minPrice,
                    [FromQuery] int? maxPrice,
                    [FromQuery] int? minPeople,
                    [FromServices] ProductController controller
                ) => controller.GetAllProducts(new ProductFiltersData
                {
                    TypeId = typeId,
                    SearchQuery = searchQuery,
                    StartLocation = startLocation,
                    EndLocation = endLocation,
                    StartDateTime = startDateTime,
                    EndDateTime = endDateTime,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinPeople = minPeople,
                    IsDeleted = isDeleted
                }))
                .WithName("GetAllProducts")
                .WithDescription("GetProduct all products")
                .Produces<ProductsResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(1)
                .WithOpenApi();

            // Create product endpoint
            app.MapPost("/product/", (
                [FromBody] ProductData data,
                [FromServices] ProductController controller
            ) => controller.CreateProduct(data))
                .WithName("CreateProduct")
                .WithDescription("Create a new product")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(3)
                .WithOpenApi();

            // Update product endpoint
            app.MapPut("/product/{productId:int}", (
                [FromRoute] int productId,
                [FromBody] ProductData data,
                [FromServices] ProductController controller
            ) => controller.UpdateProduct(productId, data))
                .WithName("UpdateProduct")
                .WithDescription("Update an existing product")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(5)
                .WithOpenApi();

            // Soft delete product endpoint
            app.MapDelete("/product/{productId:int}", (
                [FromRoute] int productId,
                [FromServices] ProductController controller
            ) => controller.SoftDeleteProduct(productId))
                .WithName("SoftDeleteProduct")
                .WithDescription("Soft delete a product by Id")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(6)
                .WithOpenApi();

            //Restore product endpoint
            app.MapPost("/product/{productId:int}/restore", (
                [FromRoute] int productId,
                [FromServices] ProductController controller
           ) => controller.RestoreProduct(productId))
                .WithName("RestoreProduct")
                .WithDescription("Restore a product by Id")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(7)
                .WithOpenApi();
        }

        private BaseResponse GetProduct(int id)
        {
            try
            {
                var product = _container.GetById(id).Result;
                if (product == null)
                {
                    return new NotFoundResponse("Product not found");
                }

                var translations = product.Translations
                    .Select(translation => new ProductTranslationResponse(
                        translation.Id,
                        translation.ProductId,
                        translation.LangIsoCode,
                        translation.Name,
                        translation.Description,
                        translation.Tags,
                        translation.IsActive
                    )).ToList();
                var dates = product.Dates
                    .Select(date => new ProductDateResponse(
                        date.Id,
                        date.Price,
                        date.StartDate,
                        date.EndDate,
                        date.Slots,
                        date.IsActive
                    ))
                    .ToList();

                var response = new ProductResponseWithDates(
                    product.Id,
                    product.StartLocation,
                    product.EndLocation,
                    product.DeletedAt,
                    product.ProductTypeId,
                    product.ProductType,
                    translations,
                    dates
                );

                return response;
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse GetAllProducts(ProductFiltersData filters)
        {
            try
            {
                var products = _container.GetAll(filters).Result;
                if (products.Count == 0)
                {
                    return new NoContentResponse();
                }

                // Transform the IEnumerable<Product> to List<ProductTypeResponse>
                var productResponses = products.Select(product =>
                {
                    var translations = product.Translations
                        .Select(translation => new ProductTranslationResponse(
                            translation.Id,
                            translation.ProductId,
                            translation.LangIsoCode,
                            translation.Name,
                            translation.Description,
                            translation.Tags,
                            translation.IsActive
                        ))
                        .ToList();
                    var dates = product.Dates
                        .Select(date => new ProductDateResponse(
                            date.Id,
                            date.Price,
                            date.StartDate,
                            date.EndDate,
                            date.Slots,
                            date.IsActive
                        ))
                        .ToList();

                    return new ProductResponseWithDates(
                        product.Id,
                        product.StartLocation,
                        product.EndLocation,
                        product.DeletedAt,
                        product.ProductTypeId,
                        product.ProductType,
                        translations,
                        dates
                    );
                }).ToList();

                // Wrap the list of ProductTypeResponse objects in a ProductsTypeResponse
                return new ProductsResponseWithDates(productResponses);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse CreateProduct(ProductData data)
        {
            try
            {
                _container.Create(data).Wait();
                return new SuccessResponse("Product created successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse UpdateProduct(int id, ProductData data)
        {
            try
            {
                _container.Update(id, data).Wait();
                return new SuccessResponse("Product updated successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse SoftDeleteProduct(int id)
        {
            try
            {
                _container.SoftDelete(id).Wait();
                return new SuccessResponse("Product soft-deleted successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse RestoreProduct(int id)
        {
            try
            {
                _container.Restore(id).Wait();
                return new SuccessResponse("Product is restored succesfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

    }
}
