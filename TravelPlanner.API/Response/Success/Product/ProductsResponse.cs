namespace TravelPlanner.API.Response.Success.Product;

public record ProductsResponse : BaseResponse
{
    public List<ProductResponse> Products { get; set; }

    public ProductsResponse(List<ProductResponse> products)
    {
        Products = products;
    }
}
