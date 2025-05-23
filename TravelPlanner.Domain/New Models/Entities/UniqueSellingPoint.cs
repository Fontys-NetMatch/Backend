using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities;

public class UniqueSellingPoint
{
    public UniqueSellingPointType Type { get; set; }
    public string? Value { get; set; }
}