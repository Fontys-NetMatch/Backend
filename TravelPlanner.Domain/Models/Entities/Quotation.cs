using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities;

[Table("Quotations")]
public record Quotation
{
    [Column, PrimaryKey, Identity]
    public int ID { get; set; }

    [Column(Length = 255), NotNull]
    public required string Name { get; set; }

    [Column, NotNull]
    public required bool IsActive { get; set; }

    [Column, NotNull]
    public required int Customer_ID { get; set; }

    [Association(ThisKey = nameof(Customer_ID), OtherKey = nameof(Customer.ID), CanBeNull = false)]
    public required Customer Customer { get; set; }

}
