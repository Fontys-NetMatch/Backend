using LinqToDB.Mapping;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.Domain.Models.Entities.Translations
{
    [Table("ProductTypeTranslations")]
    public record ProductTypeTranslation
    {

        [Column, PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Length = 100), NotNull]
        public string Name { get; set; } = null!;

        [Column(Length = 10), NotNull]
        public string LangIsoCode { get; set; } = null!;

        [Column, NotNull]
        public bool IsActive { get; set; }

        [Column, NotNull]
        public required int ProductTypeId { get; set; }

        [Association(ThisKey = nameof(ProductTypeId), OtherKey = nameof(ProductType.Id), CanBeNull = false)]
        public ProductType ProductType { get; set; } = null!;
    }
}
