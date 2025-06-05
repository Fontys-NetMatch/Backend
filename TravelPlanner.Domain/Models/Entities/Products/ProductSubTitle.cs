using LinqToDB.Mapping;
using LinqToDB.Data;

namespace TravelPlanner.Domain.Models.Entities.Products
{
    [Table("ProductSubTitles")]
    public class ProductSubTitle
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column, NotNull]
        public int TransportTypeId { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? SubTitle { get; set; }
    }
}
