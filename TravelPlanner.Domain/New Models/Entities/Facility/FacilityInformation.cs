using System.Collections.Generic;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Facility;

public class FacilityInformation
{
    public string? Id { get; set; }
    public FacilityType FacilityType { get; set; }
    public string? Value { get; set; }
    public List<string>? Values { get; set; }
}