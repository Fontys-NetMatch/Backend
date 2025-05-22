using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.Products
{
    [Table("ProductData")]
    public class Productdata
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column, NotNull]
        public string StartLocation { get; set; } = null!;

        [Column, NotNull]
        public string EndLocation { get; set; } = null!;

        [Column, NotNull]
        public int ProductTypeId { get; set; }

        [Column, NotNull]
        public DateTime DeletedAt { get; set; }
    }
}
