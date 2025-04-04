namespace TravelPlanner.Domain.Models.Request.Product;

public class ProductData
{

    public required string Location { get; set; }
    public required decimal Taxes { get; set; }
    public required int ProductType_ID { get; set; }
    public required bool IsActive { get; set; }

}