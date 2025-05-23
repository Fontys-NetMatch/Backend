using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Transport;

public class TransportInformation
{
    public TransportTypeValue TransportType { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public TransportSegment Remark { get; set; }
    public List<TransportInformationDescription> Descriptions { get; set; }
}