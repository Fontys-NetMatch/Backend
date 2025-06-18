namespace TravelPlanner.Domain.Models.Request.Quotation;

public class QuotationData
{

    public required int UserId { get; set; }
    public required string Name { get; set; }
    public required int CustomerId { get; set; }
    
    public required List<QuotationItemData> Items { get; set; }

}