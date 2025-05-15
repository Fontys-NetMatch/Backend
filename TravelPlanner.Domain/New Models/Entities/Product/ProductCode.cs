using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Product;

public class ProductCode
{
    public TransportTypeValue TransportType { get; set; }
    public string? Code { get; set; }
}