namespace TravelPlanner.Domain.Models.Request.ProductTranslation;

public class ProductTranslationData
{

    public string LangIsoCode { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }

}