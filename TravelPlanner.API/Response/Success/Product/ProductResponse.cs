using TravelPlanner.API.Response.Success.ProductTranslation;

namespace TravelPlanner.API.Response.Success.Product;

public record ProductResponse : BaseResponse
{

    public int? ID { get; set; }

    public string StartLocation { get; set; } = null!;

    public string? EndLocation { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsActive { get; set; }

    public int ProductTypeId { get; set; }
    public int ProductType_ID { get; set; }

    public List<ProductTranslationResponse> Translations { get; set; } = null!;

    protected ProductResponse()
    {
    }

    public ProductResponse(
        int id,
        string startLocation,
        string? endLocation,
        DateTime? deletedAt,
        bool isActive,
        int productTypeId,
        List<ProductTranslationResponse> translations
    ){
        ID = id;
        StartLocation = startLocation;
        EndLocation = endLocation;
        DeletedAt = deletedAt;
        ProductType_ID = productTypeId;
        IsActive = isActive;
        ProductTypeId = productTypeId;
        Translations = translations;
    }

}