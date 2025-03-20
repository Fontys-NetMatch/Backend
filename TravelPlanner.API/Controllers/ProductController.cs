using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Infrastructure.Extensions;
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
            // Create product endpoint
            app.MapPost("/product/", (
                HttpContext context,
                [FromBody] Product product,
                [FromServices] ProductController controller
            ) => controller.CreateProduct(context, product))
                .WithName("CreateProduct")
                .WithDescription("Create a new product")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithOpenApi();
                

            // Get product by ID endpoint
            app.MapGet("/product/{id}", (
                HttpContext context,
                [FromRoute] int id,
                [FromServices] ProductController controller
            ) => controller.GetProduct(context, id))
                .WithName("GetProduct")
                .WithDescription("Get a product by ID")
                .Produces<ProductResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithOpenApi();

            // Update product endpoint
            app.MapPut("/product/", (
                HttpContext context,
                [FromBody] Product product,
                [FromServices] ProductController controller
            ) => controller.UpdateProduct(context, product))
                .WithName("UpdateProduct")
                .WithDescription("Update an existing product")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithOpenApi();

            // Soft delete product endpoint
            app.MapDelete("/product/{id}", (
                HttpContext context,
                [FromRoute] int id,
                [FromServices] ProductController controller
            ) => controller.SoftDeleteProduct(context, id))
                .WithName("SoftDeleteProduct")
                .WithDescription("Soft delete a product by ID")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithOpenApi();

            // Get all active products endpoint
            app.MapGet("/products/active", (
                HttpContext context,
                [FromServices] ProductController controller
            ) => controller.GetAllActiveProducts(context))
                .WithName("GetAllActiveProducts")
                .WithDescription("Get all active products")
                .Produces<ProductsResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
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

                var response = new ProductResponse(
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

        private BaseResponse UpdateProduct(HttpContext? context, Product product)
        {
            try
            {
                _container.UpdateProduct(product).Wait();
                return new SuccessResponse("Product updated successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse SoftDeleteProduct(HttpContext? context, int id)
        {
            try
            {
                _container.SoftDeleteProduct(id).Wait();
                return new SuccessResponse("Product soft-deleted successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse GetAllActiveProducts(HttpContext? context)
        {
            try
            {
                var products = _container.GetAllActiveProductsAsync().Result;
                if (products == null || !products.Any())
                {
                    return new ErrorResponse("No active products found");
                }

                // Transform the IEnumerable<Product> to List<ProductResponse>
                var productResponses = products.Select(product => new ProductResponse(
                    id: product.ID,
                    location: product.Location,
                    taxes: product.Taxes,
                    deletedAt: product.DeletedAt,
                    isActive: product.IsActive,
                    productType_ID: product.ProductType_ID,
                    message: "Product found" // Assuming you want a message per product
                )).ToList();

                // Wrap the list of ProductResponse objects in a ProductsResponse
                return new ProductsResponse(productResponses, "Active products retrieved successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

    }
}
