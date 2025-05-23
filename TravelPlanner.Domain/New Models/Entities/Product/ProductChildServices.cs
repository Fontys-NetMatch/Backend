using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Product;

public class ProductChildServices
{
    public TransportTypeValue TransportType { get; set; }
    public List<ProductInformation> ChildServices { get; set; }
}