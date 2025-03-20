using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities.Product;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.Domain.Models.Entities
{
    [Table("AddonDates")]
    public record AddonDate
    {

        [Column, PrimaryKey, Identity]
        public int ID { get; set; }

        [Column, NotNull]
        public decimal Price { get; set; }

        [Column, Nullable]
        public int? Slots { get; set; }

        [Column, NotNull]
        public required int ProductDate_ID { get; set; }

        [Association(ThisKey = nameof(ProductDate_ID), OtherKey = nameof(ProductDate.ID), CanBeNull = false)]
        public required ProductDate ProductDate { get; set; }

        [Column, NotNull]
        public required int ProductAddon_ID { get; set; }

        [Association(ThisKey = nameof(ProductAddon_ID), OtherKey = nameof(ProductAddon.ID), CanBeNull = false)]
        public required ProductAddon ProductAddon { get; set; }
    }
}