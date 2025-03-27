namespace TravelPlanner.API.Response.Success.ProductType;

public record ProductsTypeResponse : BaseResponse
{
    public List<ProductTypeResponse> ProductTypes { get; set; }

    public ProductsTypeResponse(List<ProductTypeResponse> productTypes, string message) : base(message)
    {
        ProductTypes = productTypes;
    }
}
