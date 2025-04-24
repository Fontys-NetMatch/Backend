using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Infrastructure.Extensions;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.API.Response.Success.Product;
using TravelPlanner.API.Response.Success.ProductType;
using TravelPlanner.API.Response.Success.ProductTypeTranslation;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Request.ProductType;

namespace TravelPlanner.API.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeContainer _container;

        public ProductTypeController(IProductTypeContainer container)
        {
            _container = container;
        }

        public static void Register(WebApplication app)
        {
            // GetProductType by Id endpoint
            app.MapGet("/product-type/{typeId:int}", (
                    [FromRoute] int typeId,
                    [FromServices] ProductTypeController controller
                ) => controller.GetProduct(typeId))
                .WithName("GetProductTypeById")
                .WithDescription("GetProduct a product type by Id")
                .Produces<ProductResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(3)
                .WithOpenApi();

            // GetProduct all active products endpoint
            app.MapGet("/product-type", (
                    [FromServices] ProductTypeController controller
                ) => controller.GetAllActiveProducts())
                .WithName("GetAllActiveProductTypes")
                .WithDescription("GetProduct all active product types")
                .Produces<ProductsResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(1)
                .WithOpenApi();

            // Create product endpoint
            app.MapPost("/product-type", (
                [FromBody] ProductTypeData data,
                [FromServices] ProductTypeController controller
            ) => controller.CreateProduct(data))
                .WithName("CreateProductType")
                .WithDescription("Create a new product type")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(2)
                .WithOpenApi();

            // Update product endpoint
            app.MapPut("/product-type/{typeId:int}", (
                [FromRoute] int typeId,
                [FromBody] ProductTypeData data,
                [FromServices] ProductTypeController controller
            ) => controller.UpdateProduct(typeId, data))
                .WithName("UpdateProductType")
                .WithDescription("Update an existing product type")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(4)
                .WithOpenApi();

            // Soft delete product endpoint
            app.MapDelete("/product-type/{typeId:int}", (
                [FromRoute] int typeId,
                [FromServices] ProductTypeController controller
            ) => controller.SoftDeleteProduct(typeId))
                .WithName("SoftDeleteProductType")
                .WithDescription("Soft delete a product type by Id")
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
                    return new NotFoundResponse("ProductType not found");
                }

                var translations = product.Translations
                    .Select(translation => new ProductTypeTranslationResponse(
                        translation.Id,
                        translation.ProductTypeId,
                        translation.LangIsoCode,
                        translation.Name,
                        translation.IsActive
                    )).ToList();

                var response = new ProductTypeResponse(
                    product.Id,
                    product.IsActive,
                    translations
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
                    return new NoContentResponse();
                }

                // Transform the IEnumerable<Product> to List<ProductTypeResponse>
                var productTypeResponses = products.Select(product =>
                {
                    var translations = product.Translations
                        .Select(translation => new ProductTypeTranslationResponse(
                            translation.Id,
                            translation.ProductTypeId,
                            translation.LangIsoCode,
                            translation.Name,
                            translation.IsActive
                        )).ToList();

                    return new ProductTypeResponse(
                        id: product.Id,
                        isActive: product.IsActive,
                        translations
                    );
                }).ToList();

                // Wrap the list of ProductTypeResponse objects in a ProductsTypeResponse
                return new ProductsTypeResponse(productTypeResponses);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse CreateProduct(ProductTypeData data)
        {
            try
            {
                _container.Create(data).Wait();
                return new SuccessResponse("ProductType created successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse UpdateProduct(int id, ProductTypeData data)
        {
            try
            {
                _container.Update(id, data).Wait();
                return new SuccessResponse("ProductType updated successfully");
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
                return new SuccessResponse("ProductType soft-deleted successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

    }
}
