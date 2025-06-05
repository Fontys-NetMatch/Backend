using LinqToDB.Mapping;
using LinqToDB.Data;

namespace TravelPlanner.Domain.Models.Entities.Transport
{
    [Table("TransportSegments")]
    public class TransportSegment
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(DataType = LinqToDB.DataType.VarChar, Length = 50), Nullable]
        public string? DepartureTime { get; set; }

        [Column(DataType = LinqToDB.DataType.VarChar, Length = 50), Nullable]
        public string? ArrivalTime { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? ArrivalPointName { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? ArrivalPointAddress { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? DeparturePointName { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? DeparturePointAddress { get; set; }
    }
}
