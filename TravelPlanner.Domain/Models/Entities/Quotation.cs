using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;
using TravelPlanner.Domain.Enums;

namespace TravelPlanner.Domain.Models.Entities;

[Table("Quotations")]
public record Quotation
{
    [Column, PrimaryKey, Identity]
    public int Id { get; set; }

    [Column(Length = 255), NotNull]
    public required string Name { get; set; }

    [Column, NotNull]
    public required QuotationStatus Status { get; set; }

    [Column, NotNull]
    public required int CustomerId { get; set; }

    [Association(ThisKey = nameof(CustomerId), OtherKey = nameof(Customer.Id), CanBeNull = false)]
    public required Customer Customer { get; set; }

    [Column, NotNull]
    public required int UserId { get; set; }

    [Association(ThisKey = nameof(UserId), OtherKey = nameof(User.Id), CanBeNull = false)]
    public required User User { get; set; }

}
