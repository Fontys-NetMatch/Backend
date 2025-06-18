namespace TravelPlanner.Domain.Models.Request.Quotation;

public class QuotationItemData
{
    
    public required int ProductId { get; set; }
    public required int ProductDateId { get; set; }
    public required int Quantity { get; set; }
    
}