using LinqToDB.Mapping;
using LinqToDB.Data;

namespace TravelPlanner.Domain.Models.Entities.Transport
{
    [Table("TransportInformationDescriptions")]
    public class TransportInformationDescription
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column, NotNull]
        public int TransportInformationId { get; set; } // FK naar TransportInformation

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? Title { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? Content { get; set; }
    }
}
