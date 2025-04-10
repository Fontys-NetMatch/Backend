using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Infrastructure.Extensions;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.API.Response.Success.Product;
using TravelPlanner.API.Response.Success.ProductTranslation;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;
using TravelPlanner.Domain.Models.Request.Product;
using TravelPlanner.Domain.Models.Request.ProductTranslation;

namespace TravelPlanner.API.Controllers
{
    public class ProductTranslationController : Controller
    {
        private readonly IProductTranslationContainer _container;

        public ProductTranslationController(IProductTranslationContainer container)
        {
            _container = container;
        }

        public static void Register(WebApplication app)
        {
            // Create product translation endpoint
            app.MapPost("/product/{productId:int}/translations", (
                [FromRoute] int productId,
                [FromBody] ProductTranslationData data,
                [FromServices] ProductTranslationController controller
            ) => controller.Create(productId, data))
                .WithName("CreateProductTranslation")
                .WithDescription("Create a new product translation")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(6)
                .WithOpenApi();

            // Update product translation endpoint
            app.MapPut("/product/{productId:int}/translations/{translationId:int}", (
                [FromRoute] int productId,
                [FromRoute] int translationId,
                [FromBody] ProductTranslationUpdateData data,
                [FromServices] ProductTranslationController controller
            ) => controller.Update(translationId, data))
                .WithName("UpdateProductTranslation")
                .WithDescription("Update an existing product translation")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(7)
                .WithOpenApi();

            // Delete product translation endpoint
            app.MapDelete("/product/{productId:int}/translations/{translationId:int}", (
                    [FromRoute] int productId,
                    [FromRoute] int translationId,
                    [FromServices] ProductTranslationController controller
                ) => controller.Delete(translationId))
                .WithName("DeleteProductTranslation")
                .WithDescription("Delete an product translation")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Product")
                .WithOrder(8)
                .WithOpenApi();

        }

        private BaseResponse Create(int productId, ProductTranslationData data)
        {
            try
            {
                _container.Create(productId, data).Wait();
                return new SuccessResponse("Product translation created successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse Update(int translationId, ProductTranslationUpdateData data)
        {
            try
            {
                _container.Update(translationId, data).Wait();
                return new SuccessResponse("Product translation updated successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private object Delete(int translationId)
        {
            try
            {
                _container.Delete(translationId).Wait();
                return new SuccessResponse("Product translation updated successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

    }
}
