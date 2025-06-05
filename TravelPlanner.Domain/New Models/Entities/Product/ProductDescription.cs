using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Product;

public class ProductDescription
{
    public string? Title { get; set; }
    public 	DescriptionCategory Description { get; set; }
    public TransportTypeValue Ford { get; set; }
    public String? Content { get; set; }
    public MediaContext? Media { get; set; }
}