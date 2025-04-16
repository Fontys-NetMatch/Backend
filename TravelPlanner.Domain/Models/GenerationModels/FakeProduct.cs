
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;
using TravelPlanner.Domain.Models.Request.Product;

namespace TravelPlanner.Domain.Models.GenerationModels;

public record FakeProduct
{

    public string StartLocation { get; set; }

    public string? EndLocation { get; set; }

    public string ProductType_Name { get; set; }

    public List<ProductTranslation> Translations { get; set; } = null!;

    public List<ProductDate> ProductDates { get; set; } = null!;

}