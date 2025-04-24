using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.Domain.Models.Entities.Products;

[Table("Products")]
public record Product
{

    [Column, PrimaryKey, Identity]
    public int Id { get; set; }

    [Column(DataType = DataType.Text, Length = 1000), NotNull]
    public string StartLocation { get; set; } = null!;

    [Column(DataType = DataType.Text, Length = 1000)]
    public string? EndLocation { get; set; }

    [Column(DataType = DataType.Int32)]
    public DateTime? DeletedAt { get; set; }

    [Column, NotNull]
    public int ProductTypeId { get; set; }

    [Association(ThisKey = nameof(ProductTypeId), OtherKey = nameof(ProductType.Id))]
    public ProductType ProductType { get; set; } = null!;

    [Association(ThisKey = nameof(Id), OtherKey = nameof(ProductDate.ProductId))]
    public List<ProductDate> Dates { get; set; } = null!;

    [Association(ThisKey = nameof(Id), OtherKey = nameof(ProductTranslation.Product_ID))]
    public List<ProductTranslation> Translations { get; init; } = null!;

}