using LinqToDB.Mapping;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities;

[Table("HotelLocationAndContactInformations")]
public record HotelLocationAndContactInformation
{
    [PrimaryKey, Identity]
    public int Id { get; set; }

    [Column(DataType = LinqToDB.DataType.VarChar, Length = 100), Nullable]
    public string? CountryCode { get; set; }

    [Column(DataType = LinqToDB.DataType.VarChar, Length = 100), Nullable]
    public string? CountryName { get; set; }

    [Column(DataType = LinqToDB.DataType.VarChar, Length = 100), Nullable]
    public string? Region { get; set; }

    [Column(DataType = LinqToDB.DataType.VarChar, Length = 100), Nullable]
    public string? Destination { get; set; }

    [Column(DataType = LinqToDB.DataType.VarChar, Length = 100), Nullable]
    public string? City { get; set; }

    [Column(DataType = LinqToDB.DataType.VarChar, Length = 100), Nullable]
    public string? Address { get; set; }

    [Column(DataType = LinqToDB.DataType.VarChar, Length = 100), Nullable]
    public string? TelephoneNumber { get; set; }

    [Column, Nullable]
    public int GeoCoordinateId { get; set; }

    [Association(ThisKey = nameof(GeoCoordinateId), OtherKey = nameof(GeoCoordinates.Id), CanBeNull = false)]
    public GeoCoordinates? GeoCoordinates { get; set; } = null!;

    [Column(DataType = LinqToDB.DataType.VarChar, Length = 100), Nullable]
    public string? PhoneNumber { get; set; }
    
    public HotelLocationAndContactInformation(string? countryCode = null, string? countryName = null, string? region = null, string? destination = null, string? city = null, string? address = null, string? telephoneNumber = null, GeoCoordinates geoCoordinates = null, string? phoneNumber = null)
    {
        CountryCode = countryCode;
        CountryName = countryName;
        Region = region;
        Destination = destination;
        City = city;
        Address = address;
        TelephoneNumber = telephoneNumber;
        GeoCoordinates = geoCoordinates;
        PhoneNumber = phoneNumber;
    }
}