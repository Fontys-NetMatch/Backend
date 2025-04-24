namespace TravelPlanner.Domain.Models.Request.ProductTranslation;

public class ProductTranslationUpdateData
{

    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

}