using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.Translations;

[Table("ProductTranslations")]
public record ProductTranslation
{

    [Column, PrimaryKey, Identity]
    public int Id { get; set; }

    [Column(Length = 10), NotNull]
    public string LangIsoCode { get; set; } = null!;

    [Column(Length = 100), NotNull]
    public string Name { get; set; } = null!;

    [Column(DataType = DataType.Text, Length = 1000)]
    public string? Description { get; set; }

    [Column(DataType = DataType.Json)]
    public List<string>? Tags { get; set; }

    [Column, NotNull]
    public bool IsActive { get; set; }

    [Column, NotNull]
    public required int ProductId { get; set; }

    [Association(ThisKey = nameof(ProductId), OtherKey = nameof(Products.Product.Id), CanBeNull = false)]
    public TravelPlanner.Domain.Models.Entities.Products.Product? Product { get; set; }


}