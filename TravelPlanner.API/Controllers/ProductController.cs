using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;

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
            app.MapPost("/product/", (
                HttpContext context,
                [FromBody] Product product,
                [FromServices] ProductController controller
            ) => controller.CreateProduct(context, product))
                .WithName("CreateProduct")
                .WithDescription("Create a new product")
                .Produces<SuccessResponse>()
                .Produces<InvalidCredentialsResponse>(StatusCodes.Status400BadRequest)
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .WithOpenApi();

            app.MapGet("/product/{id}", (
                HttpContext context,
                [FromRoute] int id,
                [FromServices] ProductController controller
            ) => controller.GetProduct(context, id))
                .WithName("GetProduct")
                .WithDescription("Get a product by ID")
                .Produces<Product>()
                .Produces<InvalidCredentialsResponse>(StatusCodes.Status400BadRequest)
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .WithOpenApi();

        }

        private BaseResponse CreateProduct(HttpContext? context, Product product)
        {
            try
            {
                _container.CreateProduct(product);
                return new SuccessResponse("Product created successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse GetProduct(HttpContext? context, int id)
        {
            try
            {
                Product? product = _container.GetProductByIdAsync(id).Result;

                if (product == null)
                {
                    return new ErrorResponse("Product not found");
                }

                ProductResponse response = new ProductResponse(
                    product.ID, 
                    product.Location, 
                    product.Taxes, 
                    product.DeletedAt, 
                    product.IsActive, 
                    product.ProductType_ID, 
                    "Product found"
                );

                return response;
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }
    }
}
