using System.Reflection.PortableExecutable;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.New_Models.Entities.Facility;
using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Entities.Program;
using TravelPlanner.Domain.New_Models.Entities.Transport;
using TravelPlanner.Domain.New_Models.Enums;
using ProductType = TravelPlanner.Domain.Models.Entities.Products.ProductType;

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
    public Characteristics Characteristics { get; set; }
    public List<DistanceInformation> Distances { get; set; }
}