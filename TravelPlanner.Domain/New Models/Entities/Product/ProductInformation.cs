using System.Reflection.PortableExecutable;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.New_Models.Entities.Facility;
using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Entities.Program;
using TravelPlanner.Domain.New_Models.Entities.Transport;
using TravelPlanner.Domain.New_Models.Enums;
using ProductType = TravelPlanner.Domain.New_Models.Enums.ProductType;

namespace TravelPlanner.Domain.New_Models.Entities.Product;

public class ProductInformation
{


    public int? GiataId { get; set; }
    public ProductType Type { get; set; }
    public List<ProductCode> Codes { get; set; }
    public string? Name { get; set; }
    public string? SubTitles { get; set; }
    public ContentSupplier ContentSupplier { get; set; }
    public List<ProductDescription>? Descriptions { get; set; }
    public List<UniqueSellingPoint>? UniqueSellingPoints { get; set; }
    public List<string>? Tips { get; set; }
    public HotelLocationAndContactInformation LocationAndContactInformation { get; set; }
    public double? StarRating { get; set; }
    public List<FacilityInformation>? Facilities { get; set; }
    public MediaContext Media { get; set; }
    public DayToDayProgram? DayToDayProgramInformation { get; set; }
    public List<ProductChildServices>? ChildServices { get; set; }
    public Discountinformation? DiscountInformation { get; set; }
    public Errata Errata { get; set; }
    public int? MinimumAmountOfTripParticipants { get; set; }
    public int? MaximumAmountOfTripParticipants { get; set; }
    public List<TransportInformation>? TransportInformation { get; set; }
    public ProductCharacteristics Characteristics { get; set; }
    public List<DistanceInformation> Distances { get; set; }
    
    public ProductInformation(ProductType type, List<ProductCode> codes, ContentSupplier contentSupplier, HotelLocationAndContactInformation locationAndContactInformation, MediaContext media,
        Errata errata, ProductCharacteristics characteristics, List<DistanceInformation> distances)
    {
        Type = type;
        Codes = codes;
        ContentSupplier = contentSupplier;
        LocationAndContactInformation = locationAndContactInformation;
        Media = media;
        Errata = errata;
        Characteristics = characteristics;
        Distances = distances;
    }
    
    public ProductInformation(int? giataId, ProductType type, List<ProductCode> codes, string? name, string? subTitles, ContentSupplier contentSupplier, 
        List<ProductDescription>? descriptions, List<UniqueSellingPoint>? uniqueSellingPoints, List<string>? tips, 
        HotelLocationAndContactInformation locationAndContactInformation, double? starRating, List<FacilityInformation>? facilities, 
        MediaContext media, DayToDayProgram? dayToDayProgramInformation, List<ProductChildServices>? childServices, 
        Discountinformation? discountInformation, Errata errata, int? minimumAmountOfTripParticipants, 
        int? maximumAmountOfTripParticipants, List<TransportInformation>? transportInformation,
        ProductCharacteristics characteristics, List<DistanceInformation> distances)
    {
        GiataId = giataId;
        Type = type;
        Codes = codes;
        Name = name;
        SubTitles = subTitles;
        ContentSupplier = contentSupplier;
        Descriptions = descriptions;
        UniqueSellingPoints = uniqueSellingPoints;
        Tips = tips;
        LocationAndContactInformation = locationAndContactInformation;
        StarRating = starRating;
        Facilities = facilities;
        Media = media;
        DayToDayProgramInformation = dayToDayProgramInformation;
        ChildServices = childServices;
        DiscountInformation = discountInformation;
        Errata = errata;
        MinimumAmountOfTripParticipants = minimumAmountOfTripParticipants;
        MaximumAmountOfTripParticipants = maximumAmountOfTripParticipants;
        TransportInformation = transportInformation;
        Characteristics = characteristics;
        Distances = distances;
    }
}