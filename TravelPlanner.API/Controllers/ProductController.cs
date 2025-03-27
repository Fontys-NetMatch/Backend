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
        // Container for handling product-related logic
        private readonly IProductContainer _container;

        // Constructor to get the product container
        public ProductController(IProductContainer container)
        {
            _container = container; // Setting up the product container
        }

        // Method to register routes for product operations
        public static void Register(WebApplication app)
        {
            // POST method to create a new product
            app.MapPost("/product/", (
                HttpContext context,  // Handles the HTTP request
                [FromBody] Product product,  // Gets the product data from the request body
                [FromServices] ProductController controller  // Dependency injection for the controller
            ) => controller.CreateProduct(context, product))  // Calls the method to create a product
                .WithName("CreateProduct")  // Endpoint name for reference
                .WithDescription("Create a new product")  // Description of the endpoint
                .Produces<SuccessResponse>()  // Success response if product is created
                .Produces<InvalidCredentialsResponse>(StatusCodes.Status400BadRequest)  // Error handling for bad request
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)  // Error handling for server errors
                .WithOpenApi();  // Adds to Swagger documentation

            // GET method to fetch a product by its ID
            app.MapGet("/product/{id}", (
                HttpContext context,  // Handles the HTTP request
                [FromRoute] int id,  // Gets the product ID from the route
                [FromServices] ProductController controller  // Dependency injection for the controller
            ) => controller.GetProduct(context, id))  // Calls the method to get the product by ID
                .WithName("GetProduct")  // Endpoint name for reference
                .WithDescription("Get a product by ID")  // Description of the endpoint
                .Produces<Product>()  // Returns the product data
                .Produces<InvalidCredentialsResponse>(StatusCodes.Status400BadRequest)  // Error handling for bad request
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)  // Error handling for server errors
                .WithOpenApi();  // Adds to Swagger documentation
        }

        // Method to create a product
        private BaseResponse CreateProduct(HttpContext? context, Product product)
        {
            try
            {
                _container.CreateProduct(product);  // Calls the container to create the product
                return new SuccessResponse("Product created successfully");  // Returns a success message
            }
            catch (Exception e)  // If an error occurs
            {
                return new ErrorResponse(e.Message);  // Returns an error message
            }
        }

        // Method to get a product by ID
        private BaseResponse GetProduct(HttpContext? context, int id)
        {
            try
            {
                Product? product = _container.GetProductByIdAsync(id).Result;  // Gets the product by ID from the container

                if (product == null)  // If no product found
                {
                    return new ErrorResponse("Product not found");  // Return error if product is not found
                }

                // Create a response with the product details
                ProductResponse response = new ProductResponse(
                    product.ID,
                    product.Location,
                    product.Taxes,
                    product.DeletedAt,
                    product.IsActive,
                    product.ProductType_ID,
                    "Product found"  // Return message when the product is found
                );

                return response;  // Return product details
            }
            catch (Exception e)  // If an error occurs
            {
                return new ErrorResponse(e.Message);  // Return error message
            }
        }
    }
}
