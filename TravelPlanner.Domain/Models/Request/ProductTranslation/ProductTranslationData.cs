namespace TravelPlanner.Domain.Models.Request.ProductTranslation;

public class ProductTranslationData
{

    public string LangIsoCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

}