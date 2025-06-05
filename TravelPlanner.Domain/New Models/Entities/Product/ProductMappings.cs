using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Product;

public class ProductMappings
{
    public int? Id { get; set; }
    public IdentifierType IdType { get; set; }
    public List<ProductCode>? Codes { get; set; }
}