using LinqToDB.Mapping;
using LinqToDB.Data;

namespace TravelPlanner.Domain.Models.Entities.Transport
{
    [Table("TransportInformations")]
    public class TransportInformation
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column, NotNull]
        public int TransportTypeValueId { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? Title { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? Content { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? Remark { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public TransportInformationDescription? Description { get; set; }


    }
}
