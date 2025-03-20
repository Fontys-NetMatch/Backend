using LinqToDB;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.Products;

[Table("ProductDates")]
public record ProductDate
{

    [Column, PrimaryKey, Identity]
    public int ID { get; set; }

    [Column, NotNull]
    public decimal Price { get; set; }

    [Column(DataType = DataType.Int32), NotNull]
    public DateTime StartDate { get; set; }

    [Column(DataType = DataType.Int32), NotNull]
    public DateTime EndDate { get; set; }

    [Column, Nullable]
    public int? Slots { get; set; }

    [Column, NotNull]
    public bool IsActive { get; set; }

    [Column, NotNull]
    public required int Product_ID { get; set; }

    [Association(ThisKey = nameof(Product_ID), OtherKey = nameof(Product.ID), CanBeNull = false)]
    public required Product Product { get; set; }

}