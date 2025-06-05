using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Infrastructure.Extensions;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.API.Response.Success.Product;
using TravelPlanner.API.Response.Success.ProductImage;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.API.Controllers;

public class ProductImageController : Controller
{
    private readonly IProductImageContainer _imageContainer;

    public ProductImageController(IProductImageContainer imageContainer)
    {
        _imageContainer = imageContainer;
    }

    public static void Register(WebApplication app)
    {
        app.MapGet("/product/{productId:int}/images", (
            [FromRoute] int productId,
            [FromServices] ProductImageController controller
        ) => controller.GetImagesByProductId(productId))
        .WithName("GetImagesByProductId")
        .WithDescription("Get all active images for a product")
        .Produces<ProductsResponse>()
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
        .RequiresJwtToken()
        .WithTags("Product Images")
        .WithOpenApi();

        app.MapPost("/product/{productId:int}/images", (
            [FromRoute] int productId,
            [FromBody] CreateImageRequestDto request,
            [FromServices] ProductImageController controller
        ) => controller.CreateImage(productId, request))
        .WithName("CreateProductImage")
        .WithDescription("Add a new image to a product")
        .Produces<SuccessResponse>()
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
        .RequiresJwtToken()
        .WithTags("Product Images")
        .WithOpenApi();

        
        app.MapDelete("/product/images/{imageId:int}", (
            [FromRoute] int imageId,
            [FromServices] ProductImageController controller
        ) => controller.DeleteImage(imageId))
        .WithName("SoftDeleteProductImage")
        .WithDescription("Soft delete an image")
        .Produces<SuccessResponse>()
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
        .RequiresJwtToken()
        .WithTags("Product Images")
        .WithOpenApi();

        app.MapPost("/product/images/{imageId:int}/restore", (
            [FromRoute] int imageId,
            [FromServices] ProductImageController controller
        ) => controller.RestoreImage(imageId))
        .WithName("RestoreProductImage")
        .WithDescription("Restore a previously deleted product image")
        .Produces<SuccessResponse>()
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
        .RequiresJwtToken()
        .WithTags("Product Images")
        .WithOpenApi();
    }

    private BaseResponse GetImagesByProductId(int productId)
    {
        try
        {
            var images = _imageContainer.GetImagesByProductIdAsync(productId).Result;

            var imageResponses = images.Select(i => new ProductImageResponse(
                i.Id,
                i.Supplier,
                i.ImageIdentifier,
                $"https://v2.nbc.sandbox.twd.travel/api/Images/{i.Supplier}/{i.ImageIdentifier}" // Constructed URL
            )).ToList();

            return new ProductImagesResponse(imageResponses, "Images retrieved successfully");
        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message);
        }
    }


    private BaseResponse CreateImage(int productId, CreateImageRequestDto request)
    {
        try
        {
            var image = new ProductImage
            {
                ProductId = productId,
                Supplier = request.Supplier,
                ImageIdentifier = request.ImageIdentifier,
                Description = request.Description
            };

            _imageContainer.Create(image).Wait();
            return new SuccessResponse("Image added successfully");
        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message);
        }
    }



    private BaseResponse DeleteImage(int imageId)
    {
        try
        {
            _imageContainer.SoftDelete(imageId).Wait();
            return new SuccessResponse("Image deleted successfully");
        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message);
        }
    }

    private BaseResponse RestoreImage(int imageId)
    {
        try
        {
            _imageContainer.Restore(imageId).Wait();
            return new SuccessResponse("Image restored successfully");
        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message);
        }
    }
}
