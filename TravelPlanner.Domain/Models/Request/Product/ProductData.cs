namespace TravelPlanner.Domain.Models.Request.Product;

public class ProductData
{

    public required string StartLocation { get; set; }
    public required string? EndLocation { get; set; }
    public required int ProductTypeId { get; set; }
    public required DateTime DeletedAt { get; set; }

}