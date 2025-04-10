using TravelPlanner.API.Response.Success.ProductTranslation;

namespace TravelPlanner.API.Response.Success.Product;

public record ProductResponse : BaseResponse
{

    public int ID { get; set; }

    public string? Departure { get; set; }

    public string? Arrival { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsActive { get; set; }

    public int ProductType_ID { get; set; }

    public ProductTranslationsResponse Translations { get; set; }

    public ProductResponse(int id, string? departure, string? arrival, DateTime? deletedAt, bool isActive, int productType_ID, ProductTranslationsResponse translations) : base("Product found")
    {
        ID = id;
        Departure = departure;
        Arrival = arrival;
        DeletedAt = deletedAt;
        IsActive = isActive;
        ProductType_ID = productType_ID;
        Translations = translations;
    }

}