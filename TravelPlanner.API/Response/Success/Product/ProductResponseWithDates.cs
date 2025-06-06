using TravelPlanner.API.Response.Success.ProductDate;
using TravelPlanner.API.Response.Success.ProductTranslation;

namespace TravelPlanner.API.Response.Success.Product;

public record ProductResponseWithDates : ProductResponse
{

    public List<ProductDateResponse> Dates { get; set; }

    public ProductResponseWithDates(
        int id,
        string startLocation,
        string? endLocation,
        DateTime? deletedAt,
        int productTypeId,
        Domain.Models.Entities.Products.ProductTypeEntity productType,
        List<ProductTranslationResponse> translations,
        List<ProductDateResponse> dates
    ){
        Id = id;
        StartLocation = startLocation;
        EndLocation = endLocation;
        DeletedAt = deletedAt;
        ProductTypeId = productTypeId;
        ProductType = productType;
        Translations = translations;
        Dates = dates;
    }

}