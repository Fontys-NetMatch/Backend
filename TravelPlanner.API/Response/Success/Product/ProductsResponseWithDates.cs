namespace TravelPlanner.API.Response.Success.Product;

public record ProductsResponseWithDates : BaseResponse
{
    public List<ProductResponseWithDates> Products { get; set; }

    public ProductsResponseWithDates(List<ProductResponseWithDates> products)
    {
        Products = products;
    }
}
