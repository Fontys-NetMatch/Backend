using TravelPlanner.API.Response.Success.ProductDate;
using TravelPlanner.API.Response.Success.ProductTranslation;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.API.Response.Success.Product;

public record ProductResponseWithDates : ProductResponse
{

    public List<ProductDateResponse> Dates { get; set; }

    public ProductResponseWithDates(
        int id,
        string startLocation,
        string? endLocation,
        DateTime? deletedAt,
        bool isActive,
        int productTypeId,
        List<ProductTranslationResponse> translations,
        List<ProductDateResponse> dates
    ){
        ID = id;
        StartLocation = startLocation;
        EndLocation = endLocation;
        DeletedAt = deletedAt;
        IsActive = isActive;
        ProductTypeId = productTypeId;
        Translations = translations;
        Dates = dates;
    }

}