using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.Domain.Models.Entities.Translations
{
    [Table("ProductTypeTranslations")]
    public record ProductTypeTranslation
    {

        [Column, PrimaryKey, Identity]
        public int ID { get; set; }

        [Column(Length = 100), NotNull]
        public string Name { get; set; }

        [Column(Length = 10), NotNull]
        public string LangIsoCode { get; set; }

        [Column, NotNull]
        public required int ProductType_ID { get; set; }

        [Association(ThisKey = nameof(ProductType_ID), OtherKey = nameof(ProductType.ID), CanBeNull = false)]
        public required ProductType ProductType { get; set; }

    }
}
