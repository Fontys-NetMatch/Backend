using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities
{
    [Table(Name = "ContentSuppliers")]
    public class ContentSupplierEntity
    {
        [Column, PrimaryKey] 
        public int Id { get; set; }

        [Column, NotNull]
        public string Name { get; set; } = null!;
    }
}
