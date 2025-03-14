using LinqToDB;
using LinqToDB.Mapping;
using TravelPlanner.Domain.Models.Entities.Products;


namespace TravelPlanner.Domain.Models.Entities;

[Table("ProductTranslations")]
public record ProductTranslation
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
    public required int Product_ID { get; set; }

    [Association(ThisKey = nameof(Product_ID), OtherKey = nameof(TravelPlanner.Domain.Models.Entities.Products.Product.ID), CanBeNull = false)]
    public required TravelPlanner.Domain.Models.Entities.Products.Product Product { get; set; }


}