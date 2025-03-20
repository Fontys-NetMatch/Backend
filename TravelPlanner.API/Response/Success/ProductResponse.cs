using LinqToDB;
using LinqToDB.Mapping;
using TravelPlanner.API.Response;

namespace TravelPlanner.API.Response.Success;

public record ProductResponse : BaseResponse
{

    public int ID { get; set; }

    public string? Location { get; set; }

    public decimal Taxes { get; set; }

    public DateTime DeletedAt { get; set; }

    public bool IsActive { get; set; }

    public int ProductType_ID { get; set; }

    public ProductResponse(int id, string location, decimal taxes, DateTime deletedAt, bool isActive, int productType_ID, string message) : base(message)
    {
        ID = id;
        Location = location;
        Taxes = taxes;
        DeletedAt = deletedAt;
        IsActive = isActive;
        ProductType_ID = productType_ID;
    }

}