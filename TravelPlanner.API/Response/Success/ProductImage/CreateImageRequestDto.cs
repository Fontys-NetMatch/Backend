namespace TravelPlanner.API.Response.Success.ProductImage
{
    public record CreateImageRequestDto(
        string Supplier,
        string ImageIdentifier,
        string? Description
    );
}
