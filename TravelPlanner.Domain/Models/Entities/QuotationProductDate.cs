using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;
using TravelPlanner.Domain.Enums;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.Domain.Models.Entities;

[Table("QuotationProductDates")]
public record QuotationProductDate
{
    [Column, PrimaryKey, Identity]
    public int ID { get; set; }

    [Column, NotNull]
    public required int ProductDate_ID { get; set; }

    [Association(ThisKey = nameof(ProductDate_ID), OtherKey = nameof(ProductDate.ID), CanBeNull = false)]
    public required ProductDate ProductDate { get; set; }

    [Column, NotNull]
    public required int Quotation_ID { get; set; }

    [Association(ThisKey = nameof(Quotation_ID), OtherKey = nameof(Quotation.ID), CanBeNull = false)]
    public required Quotation Quotation { get; set; }

}
