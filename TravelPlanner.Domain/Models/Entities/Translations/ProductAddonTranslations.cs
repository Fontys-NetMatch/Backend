using LinqToDB;
using LinqToDB.Mapping;
using TravelPlanner.Domain.Models.Entities.Product;
using TravelPlanner.Domain.Models.Entities.Products;


namespace TravelPlanner.Domain.Models.Entities;

[Table("ProductAddonTranslations")]
public record ProductAddonTranslation
{

    [Column, PrimaryKey, Identity]
    public int ID { get; set; }

    [Column(Length = 100), NotNull]
    public string Name { get; set; }

    [Column(Length = 10), NotNull]
    public string LangIsoCode { get; set; }

    [Column(DataType = DataType.Text, Length = 1000), NotNull]
    public string Description { get; set; }

    [Column, NotNull]
    public bool IsActive { get; set; }

    [Column, NotNull]
    public required int ProductAddon_ID { get; set; }

    [Association(ThisKey = nameof(ProductAddon_ID), OtherKey = nameof(ProductAddon.ID), CanBeNull = false)]
    public required ProductAddon Product { get; set; } 


}