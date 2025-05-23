using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.Products;

[Table("ProductImages")]
public record ProductImage
{
    public ProductImage(string path)
    {
        Path = path;
    }

    [Column, PrimaryKey, Identity]
    public int Id { get; set; }

    [Column(Length = 500), NotNull]
    public string Path { get; set; }

    [Column(DataType = DataType.Int32), NotNull]
    public DateTime? DeletedAt { get; set; }

    [Column, NotNull]
    public required int ProductId { get; set; }

    [Association(ThisKey = nameof(ProductId), OtherKey = nameof(Product.Id), CanBeNull = false)]
    public Product Product { get; set; } = null!;

}