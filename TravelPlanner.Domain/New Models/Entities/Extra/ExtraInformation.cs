using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.New_Models.Entities.Media;

namespace TravelPlanner.Domain.New_Models.Entities.Extra;

public class ExtraInformation
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? ContentTitle { get; set; }
    public string? Description { get; set; }
    public List<MediaContext>? Media { get; set; }
    public string Category { get; set; }
    public List<ExtraOption>? Options { get; set; } 
}