using System.Collections.Generic;
using TravelPlanner.API.Response;

namespace TravelPlanner.API.Response.Success;

public record ProductsResponse : BaseResponse
{
    public List<ProductResponse> Products { get; set; }

    public ProductsResponse(List<ProductResponse> products, string message) : base(message)
    {
        Products = products;
    }
}
