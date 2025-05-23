namespace TravelPlanner.Domain.New_Models.Entities;

public class GeoCoordinates
{


    public double Longitude { get; set; }
    
    public double Latitude { get; set; }
    
    public GeoCoordinates(double longitude, double latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
    }
}