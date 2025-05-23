using TravelPlanner.API.Response;

namespace TravelPlanner.API.Response.Success.ProductImage
{
    public record ProductImageResponse(
        int Id,
        string Supplier,
        string ImageIdentifier,
        string Url
    );

   
}
