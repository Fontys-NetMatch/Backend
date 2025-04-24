using TravelPlanner.API.Response.Success.ProductTranslation;

namespace TravelPlanner.API.Response.Success.Product;

public record ProductResponse : BaseResponse
{

    public int? Id { get; set; }

    public string StartLocation { get; set; } = null!;

    public string? EndLocation { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int ProductTypeId { get; set; }

    public Domain.Models.Entities.Products.ProductType ProductType { get; set; } = null!;

    public List<ProductTranslationResponse> Translations { get; set; } = null!;

    protected ProductResponse()
    {
    }

    public ProductResponse(
        int id,
        string startLocation,
        string? endLocation,
        DateTime? deletedAt,
        int productTypeId,
        Domain.Models.Entities.Products.ProductType productType,
        List<ProductTranslationResponse> translations
    ){
        Id = id;
        StartLocation = startLocation;
        EndLocation = endLocation;
        DeletedAt = deletedAt;
        ProductTypeId = productTypeId;
        ProductType = productType;
        Translations = translations;
    }

}