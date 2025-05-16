using System.Diagnostics;
using TravelPlanner.Domain.New_Models.Entities.Media;

namespace TravelPlanner.Domain.New_Models.Entities.Program;

public class DayProgram
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int? StartDay { get; set; }
    public int? EndDay { get; set; }
    public MediaContext? Media { get; set; }
    public GeoCoordinates? Coordinates { get; set; }
    public int? TravelDistance { get; set; }
}