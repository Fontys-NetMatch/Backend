
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;
using TravelPlanner.Domain.Models.Request.Product;

namespace TravelPlanner.Domain.Models.GenerationModels;

public record FakeProduct
{

    public string StartLocation { get; init; } = null!;

    public string? EndLocation { get; init; }

    public string ProductTypeName { get; init; } = null!;

    public List<ProductTranslation> Translations { get; init; } = null!;

    public List<ProductDate> ProductDates { get; init; } = null!;

}