using LinqToDB.Mapping;
using TravelPlanner.Domain.Models.Entities.Product;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.Domain.Models.Entities
{
    [Table("AddonDates")]
    public record AddonDate
    {

        [Column, PrimaryKey, Identity]
        public int Id { get; set; }

        [Column, NotNull]
        public decimal Price { get; set; }

        [Column, Nullable]
        public int? Slots { get; set; }

        [Column, NotNull]
        public required int ProductDateId { get; set; }

        [Association(ThisKey = nameof(ProductDateId), OtherKey = nameof(ProductDate.Id), CanBeNull = false)]
        public required ProductDate ProductDate { get; set; }

        [Column, NotNull]
        public required int ProductAddonId { get; set; }

        [Association(ThisKey = nameof(ProductAddonId), OtherKey = nameof(ProductAddon.Id), CanBeNull = false)]
        public required ProductAddon ProductAddon { get; set; }
    }
}