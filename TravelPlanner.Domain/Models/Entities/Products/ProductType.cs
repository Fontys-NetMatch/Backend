using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.Domain.Models.Entities.Products;

[Table("ProductTypes")]
public record ProductType
{

    [Column, PrimaryKey, Identity]
    public int ID { get; set; }

    [Column, NotNull]
    public bool IsActive { get; set; }

    [Association(ThisKey = nameof(ID), OtherKey = nameof(ProductTypeTranslation.ProductType_ID))]
    public List<ProductTypeTranslation> Translations { get; set; } = null!;

}