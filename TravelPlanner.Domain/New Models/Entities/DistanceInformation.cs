namespace TravelPlanner.Domain.New_Models.Entities;

public class DistanceInformation
{


    public string? PointOfInterestType { get; set; }
    public string? PointOfInterest { get; set; }
    public double? Distance { get; set; }
    public string? Description { get; set; }
    
    public DistanceInformation(string? pointOfInterestType = null, string? pointOfInterest = null, double? distance = default, string? description = null)
    {
        PointOfInterestType = pointOfInterestType;
        PointOfInterest = pointOfInterest;
        Distance = distance;
        Description = description;
    }
}