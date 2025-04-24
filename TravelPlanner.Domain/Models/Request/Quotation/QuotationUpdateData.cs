using TravelPlanner.Domain.Enums;

namespace TravelPlanner.Domain.Models.Request.Quotation;

public class QuotationUpdateData
{

    public required int Id { get; set; }
    public required string Name { get; set; }
    public required QuotationStatus Status { get; set; }
    public required int CustomerId { get; set; }

}