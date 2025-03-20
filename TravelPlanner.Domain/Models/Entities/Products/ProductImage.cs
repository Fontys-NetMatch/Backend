using LinqToDB;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.Products;

[Table("ProductImages")]
public record ProductImage
{

    [Column, PrimaryKey, Identity]
    public int ID { get; set; }

    [Column(Length = 500), NotNull]
    public string Path { get; set; }

    [Column(DataType = DataType.Int32), NotNull]
    public DateTime DeletedAt { get; set; }

    [Column, NotNull]
    public bool IsActive { get; set; }

    [Column, NotNull]
    public required int Product_ID { get; set; }

    [Association(ThisKey = nameof(Product_ID), OtherKey = nameof(Product.ID), CanBeNull = false)]
    public required Product Product { get; set; }

}