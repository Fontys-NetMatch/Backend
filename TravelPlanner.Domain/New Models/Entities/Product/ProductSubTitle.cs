using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Product;

public class ProductSubTitle
{
    public TransportTypeValue TransportType { get; set; }
    public string? SubTitle { get; set; }
}