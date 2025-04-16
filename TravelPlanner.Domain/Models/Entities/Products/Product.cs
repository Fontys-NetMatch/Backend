using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.Domain.Models.Entities.Products;

[Table("Products")]
public record Product
{

    [Column, PrimaryKey, Identity]
    public int ID { get; set; }

    [Column(DataType = DataType.Text, Length = 1000)]
    public string StartLocation { get; set; }

    [Column(DataType = DataType.Text, Length = 1000)]
    public string? EndLocation { get; set; }

    [Column(DataType = DataType.Int32)]
    public DateTime? DeletedAt { get; set; }

    [Column, NotNull]
    public bool IsActive { get; set; }

    [Column, NotNull]
    public int ProductType_ID { get; set; }

    [Association(ThisKey = nameof(ProductType_ID), OtherKey = nameof(ProductType.ID))]
    public ProductType ProductType { get; set; } = null!;

    [Association(ThisKey = nameof(ID), OtherKey = nameof(ProductDate.Product_ID))]
    public List<ProductDate> Dates { get; set; } = null!;

    [Association(ThisKey = nameof(ID), OtherKey = nameof(ProductTranslation.Product_ID))]
    public List<ProductTranslation> Translations { get; set; } = null!;

}