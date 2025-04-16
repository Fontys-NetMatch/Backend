namespace TravelPlanner.Domain.Models.Request.Product;

public class ProductData
{

    public required string StartLocation { get; set; }
    public required string? EndLocation { get; set; }
    public required int ProductType_ID { get; set; }
    public required bool IsActive { get; set; }

}