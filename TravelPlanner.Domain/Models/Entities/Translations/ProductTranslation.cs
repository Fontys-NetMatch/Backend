using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.Translations;

[Table("ProductTranslations")]
public record ProductTranslation
{

    [Column, PrimaryKey, Identity]
    public int ID { get; set; }

    [Column(Length = 10), NotNull]
    public string LangIsoCode { get; set; }

    [Column(Length = 100), NotNull]
    public string Name { get; set; }

    [Column(DataType = DataType.Text, Length = 1000)]
    public string? Description { get; set; }

    [Column(DataType = DataType.Json)]
    public List<string>? Tags { get; set; }

    [Column, NotNull]
    public bool IsActive { get; set; }

    [Column, NotNull]
    public required int Product_ID { get; set; }

    [Association(ThisKey = nameof(Product_ID), OtherKey = nameof(TravelPlanner.Domain.Models.Entities.Products.Product.Id), CanBeNull = false)]
    public TravelPlanner.Domain.Models.Entities.Products.Product? Product { get; set; }


}