using LinqToDB;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities;

[Table("Products")]
public record Product
{

    [Column, PrimaryKey, Identity]
    public int ID { get; set; }

    [Column(Length = 100), NotNull]
    public string? Location { get; set; }

    [Column, NotNull]
    public decimal Taxes { get; set; }

    [Column(DataType = DataType.Int32), NotNull]
    public DateTime DeletedAt { get; set; }

    [Column, NotNull]
    public bool IsActive { get; set; }

    [Column, NotNull]
    public int ProductType_ID { get; set; }

}