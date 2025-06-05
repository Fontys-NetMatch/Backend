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
    
    public HotelLocationAndContactInformation(string? countryCode = null, string? countryName = null, string? region = null, string? destination = null, string? city = null, string? address = null, string? telephoneNumber = null, GeoCoordinates coordinates = null, string? phoneNumber = null)
    {
        CountryCode = countryCode;
        CountryName = countryName;
        Region = region;
        Destination = destination;
        City = city;
        Address = address;
        TelephoneNumber = telephoneNumber;
        Coordinates = coordinates;
        PhoneNumber = phoneNumber;
    }
}