/*using System.Text.Json;
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


                    app.MapGet("/product/all", (
                    HttpContext context,
                    [FromServices] ProductController controller
                    ) => controller.GetAllProducts(context))
                    .WithName("GetAllProducts")
                    .WithDescription("Get all active products")
                    .Produces<BaseResponse>() // of .Produces<IEnumerable<Product>>()
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
}*/


using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.Domain.Interfaces.BLL;
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
            // ✅ POST /product/
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

            // ✅ PUT /product/{id}
            app.MapPut("/product/{id}", (
                HttpContext context,
                [FromRoute] int id,
                [FromBody] Product product,
                [FromServices] ProductController controller
            ) => controller.UpdateProduct(context, id, product))
                .WithName("UpdateProduct")
                .WithDescription("Update an existing product")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .WithOpenApi();


            // ✅ GET /product/{id}
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

            // ✅ NIEUW: GET /product/all
            app.MapGet("/product/all", (
                HttpContext context,
                [FromServices] ProductController controller
            ) => controller.GetAllProducts(context))
                .WithName("GetAllProducts")
                .WithDescription("Get all active products")
                .Produces<BaseResponse>()
                .WithOpenApi();

            // ✅ DELETE (Soft) /product/{id}
            app.MapDelete("/product/{id}", (
                HttpContext context,
                [FromRoute] int id,
                [FromServices] ProductController controller
            ) => controller.SoftDeleteProduct(context, id))
            .WithName("SoftDeleteProduct")
            .WithDescription("Soft delete a product")
            .Produces<SuccessResponse>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

        }

        // ✅ POST: Create product
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

        // ✅ GET: Single product by ID
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

        // ✅ NIEUW: Get all active products
        private BaseResponse GetAllProducts(HttpContext? context)
        {
            try
            {
                var products = _container.GetAllActiveProductsAsync().Result;

                return new BaseResponse("Actieve producten opgehaald")
                {
                    StatusCode = 200,
                    Data = new Dictionary<string, object?>
            {
                { "products", products } // ✅ zet de lijst onder een key
            }
                };
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
                var product = _container.GetProductByIdAsync(id).Result;

                if (product == null)
                {
                    return new ErrorResponse("Product niet gevonden");
                }

                _container.SoftDeleteProduct(id);
                return new SuccessResponse("Product succesvol verwijderd");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }


        private BaseResponse UpdateProduct(HttpContext? context, int id, Product updatedProduct)
        {
            try
            {
                var existing = _container.GetProductByIdAsync(id).Result;
                if (existing == null)
                {
                    return new ErrorResponse("Product not found");
                }

                updatedProduct.ID = id; // Zorg dat ID correct blijft

                _container.UpdateProduct(updatedProduct);
                return new SuccessResponse("Product updated successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }


    }
}

