using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities;

public class Discountinformation
{
    public DiscountType Type { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}