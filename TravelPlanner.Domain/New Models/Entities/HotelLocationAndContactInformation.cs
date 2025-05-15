namespace TravelPlanner.Domain.New_Models.Entities;

public class HotelLocationAndContactInformation
{
    public string? CountryCode { get; set; }
    public string? CountryName { get; set; }
    public string? Region { get; set; }
    public string? Destination { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? TelephoneNumber { get; set; }
    public GeoCoordinates Coordinates { get; set; }
    public string? PhoneNumber { get; set; }
}