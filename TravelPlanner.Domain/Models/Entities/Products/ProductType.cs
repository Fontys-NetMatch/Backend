using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.Domain.Models.Entities.Products;

[Table("ProductTypes")]
public record ProductType
{

    [Column, PrimaryKey, Identity]
    public int Id { get; set; }

    [Column, NotNull]
    public bool IsActive { get; set; }

    [Association(ThisKey = nameof(Id), OtherKey = nameof(ProductTypeTranslation.ProductTypeId))]
    public List<ProductTypeTranslation> Translations { get; init; } = null!;

}