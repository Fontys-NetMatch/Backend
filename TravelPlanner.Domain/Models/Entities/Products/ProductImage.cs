using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.Products;

[Table("ProductImages")]
public record ProductImage
{
    [Column, PrimaryKey, Identity]
    public int Id { get; set; }

    [Column(Length = 100), NotNull]
    public string Supplier { get; set; } = null!;  // e.g., "Corendon"

    [Column(Length = 100), NotNull]
    public string ImageIdentifier { get; set; } = null!;  // e.g., "abc123"

    [Column(Length = 500), NotNull]
    public string? Description { get; set; }  // Optional

    [Column(DataType = DataType.DateTime)]
    public DateTime? DeletedAt { get; set; }

    [Column, NotNull]
    public int ProductId { get; set; }

    [Association(ThisKey = nameof(ProductId), OtherKey = nameof(Product.Id))]
    public Product Product { get; set; } = null!;
}