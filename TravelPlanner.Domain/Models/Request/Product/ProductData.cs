namespace TravelPlanner.Domain.Models.Request.Product;

public class ProductData
{
    
    public Dictionary<string, string> NameTranslations { get; set; }
    public string Location { get; set; }
    public decimal Taxes { get; set; }
    public int ProductType_ID { get; set; }

}