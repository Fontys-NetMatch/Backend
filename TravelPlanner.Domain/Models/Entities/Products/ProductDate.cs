using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.Products;

[Table("ProductDates")]
public record ProductDate
{

    [Column, PrimaryKey, Identity]
    public int Id { get; set; }

    [Column(DataType = DataType.Decimal, Precision = 20, Scale = 2), NotNull]
    public double Price { get; init; }

    [Column(DataType = DataType.Int32), NotNull]
    public DateTime StartDate { get; init; }

    [Column(DataType = DataType.Int32), NotNull]
    public DateTime EndDate { get; init; }

    [Column, Nullable]
    public int? Slots { get; init; }

    [Column, NotNull]
    public bool IsActive { get; set; }

    [Column, NotNull]
    public required int ProductId { get; set; }

    [Association(ThisKey = nameof(ProductId), OtherKey = nameof(Product.Id), CanBeNull = false)]
    public Product Product { get; set; } = null!;
}