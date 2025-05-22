namespace TravelPlanner.Domain.Models.Request.Transport
{
    public class TransportSegmentData
    {
        public string? DepartureTime { get; set; }
        public string? ArrivalTime { get; set; }
        public string? ArrivalPointName { get; set; }
        public string? ArrivalPointAddress { get; set; }
        public string? DeparturePointName { get; set; }
        public string? DeparturePointAddress { get; set; }
    }
}
