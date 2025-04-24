using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;
using TravelPlanner.Domain.Models.Entities.Product;
using TravelPlanner.Domain.Models.Entities.Products;


namespace TravelPlanner.Domain.Models.Entities;

[Table("ProductAddonTranslations")]
public record ProductAddonTranslation
{

    [Column, PrimaryKey, Identity]
    public int Id { get; set; }

    [Column(Length = 100), NotNull]
    public string Name { get; set; } = null!;

    [Column(Length = 10), NotNull]
    public string LangIsoCode { get; set; } = null!;

    [Column(DataType = DataType.Text, Length = 1000), NotNull]
    public string Description { get; set; } = null!;

    [Column, NotNull]
    public bool IsActive { get; set; }

    [Column, NotNull]
    public required int ProductAddonId { get; set; }

    [Association(ThisKey = nameof(ProductAddonId), OtherKey = nameof(ProductAddon.Id), CanBeNull = false)]
    public ProductAddon Product { get; set; } = null!;


}