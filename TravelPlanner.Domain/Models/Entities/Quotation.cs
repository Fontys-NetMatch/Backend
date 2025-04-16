using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;
using TravelPlanner.Domain.Enums;

namespace TravelPlanner.Domain.Models.Entities;

[Table("Quotations")]
public record Quotation
{
    [Column, PrimaryKey, Identity]
    public int ID { get; set; }

    [Column(Length = 255), NotNull]
    public required string Name { get; set; }

    [Column, NotNull]
    public required QuotationStatus Status { get; set; }

    [Column, NotNull]
    public required int Customer_ID { get; set; }

    [Association(ThisKey = nameof(Customer_ID), OtherKey = nameof(Customer.ID), CanBeNull = false)]
    public required Customer Customer { get; set; }

    [Column, NotNull]
    public required int User_ID { get; set; }

    [Association(ThisKey = nameof(User_ID), OtherKey = nameof(User.ID), CanBeNull = false)]
    public required User User { get; set; }

}
