namespace TravelPlanner.Domain.Models.Request.Product;

public class ProductData
{

    public required string Departure { get; set; }
    public required string Arrival { get; set; }
    public required int ProductType_ID { get; set; }
    public required bool IsActive { get; set; }

}