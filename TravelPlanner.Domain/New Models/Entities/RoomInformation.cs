using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.New_Models.Entities.Facility;
using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities;

public class RoomInformation
{
    public RoomType Type { get; set; }
    public string? Name { get; set; }
    public string? HotelProvider { get; set; }
    public string? HotelCode { get; set; }
    public string? PeakworkIdentifier { get; set; }
    public List<string>? Keys { get; set; }
    public string? SubCode { get; set; }
    public MediaContext? Media { get; set; }
    public List<FacilityInformation>? Facilities { get; set; }
    public int? StandardOccupancy { get; set; }
    public int? MinOccupancy { get; set; }
    public int? MaxOccupancy { get; set; }
    public int? MinAdultOccupancy { get; set; }
    public int? MaxAdultOccupancy { get; set; }
    public int? MinChildOccupancy { get; set; }
    public int? MaxChildOccupancy { get; set; }
    public int? MinInfantOccupancy { get; set; }
    public int? MaxInfantOccupancy { get; set; }
    public string? Description { get; set; }
}