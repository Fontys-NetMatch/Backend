using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Infrastructure.Extensions;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.API.Response.Success.Product;
using TravelPlanner.API.Response.Success.ProductTranslation;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities.Products;
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

            // GetProduct product by ID endpoint
            app.MapGet("/product/{productId:int}", (
                    [FromRoute] int productId,
                    [FromServices] ProductController controller
                ) => controller.GetProduct(productId))
                .WithName("GetProductById")
                .WithDescription("GetProduct a product by ID")
                .Produces<ProductResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(3)
                .WithOpenApi();

            // GetProduct all active products endpoint
            app.MapGet("/product", (
                    [FromServices] ProductController controller
                ) => controller.GetAllActiveProducts())
                .WithName("GetAllActiveProducts")
                .WithDescription("GetProduct all active products")
                .Produces<ProductsResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(1)
                .WithOpenApi();

            // Create product endpoint
            app.MapPost("/product", (
                [FromBody] ProductData data,
                [FromServices] ProductController controller
            ) => controller.CreateProduct(data))
                .WithName("CreateProduct")
                .WithDescription("Create a new product")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(2)
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
                .WithOrder(4)
                .WithOpenApi();

            // Soft delete product endpoint
            app.MapDelete("/product/{productId:int}", (
                [FromRoute] int productId,
                [FromServices] ProductController controller
            ) => controller.SoftDeleteProduct(productId))
                .WithName("SoftDeleteProduct")
                .WithDescription("Soft delete a product by ID")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(5)
                .WithOpenApi();
        }

        private BaseResponse GetProduct(int id)
        {
            try
            {
                var product = _container.GetById(id).Result;
                if (product == null)
                {
                    return new ErrorResponse("Product not found");
                }

                var translations = product.Translations
                    .Select(translation => new ProductTranslationResponse(
                        translation.ID,
                        translation.Product_ID,
                        translation.LangIsoCode,
                        translation.Name,
                        translation.Description,
                        translation.IsActive
                    )).ToList();

                var response = new ProductResponse(
                    product.ID,
                    product.Location,
                    product.Taxes,
                    product.DeletedAt,
                    product.IsActive,
                    product.ProductType_ID,
                    new ProductTranslationsResponse(translations, "Product translations retrieved successfully")
                );

                return response;
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse GetAllActiveProducts()
        {
            try
            {
                var products = _container.GetAllActive().Result;
                if (products.Count == 0)
                {
                    return new ErrorResponse("No active products found");
                }

                // Transform the IEnumerable<Product> to List<ProductResponse>
                var productResponses = products.Select(product =>
                {
                    var translations = product.Translations
                        .Select(translation => new ProductTranslationResponse(
                            translation.ID,
                            translation.Product_ID,
                            translation.LangIsoCode,
                            translation.Name,
                            translation.Description,
                            translation.IsActive
                        )).ToList();

                    return new ProductResponse(
                        id: product.ID,
                        location: product.Location,
                        taxes: product.Taxes,
                        deletedAt: product.DeletedAt,
                        isActive: product.IsActive,
                        productType_ID: product.ProductType_ID,
                        new ProductTranslationsResponse(translations, "Product translations retrieved successfully")
                    );
                }).ToList();

                // Wrap the list of ProductResponse objects in a ProductsResponse
                return new ProductsResponse(productResponses, "Active products retrieved successfully");
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

    }
}
