using TravelPlanner.API.Response.Success.ProductTranslation;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.API.Response.Success.Product;

public record ProductResponse : BaseResponse
{

    public int ID { get; set; }

    public string StartLocation { get; set; }

    public string? EndLocation { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int ProductType_ID { get; set; }

    public ProductTranslationsResponse Translations { get; set; }

    public ProductResponse(int id, string startLocation, string? endLocation, DateTime? deletedAt, int productType_ID, ProductTranslationsResponse translations) : base("Product found")
    {
        ID = id;
        StartLocation = startLocation;
        EndLocation = endLocation;
        DeletedAt = deletedAt;
        ProductType_ID = productType_ID;
        Translations = translations;
    }

}