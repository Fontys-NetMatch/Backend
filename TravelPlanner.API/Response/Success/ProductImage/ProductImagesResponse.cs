namespace TravelPlanner.API.Response.Success.ProductImage
{
    public record ProductImagesResponse(
       List<ProductImageResponse> Images,
       string Message
   ) : BaseResponse;
}
