using System.ComponentModel;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.Product
{
    [Table("ProductAddons")]
    public record ProductAddon
    {

        [Column, PrimaryKey, Identity]
        public int ID { get; set; }

        [Column, NotNull]
        public bool IsActive { get; set; }
    }
}