using LinqToDB.Mapping;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.Domain.Models.Entities;

[Table("QuotationProductDates")]
public record QuotationProductDate
{

    [Column, NotNull]
    public required int ProductDateId { get; set; }

    [Association(ThisKey = nameof(ProductDateId), OtherKey = nameof(ProductDate.Id), CanBeNull = false)]
    public required ProductDate ProductDate { get; set; }

    [Column, NotNull]
    public required int QuotationId { get; set; }

    [Association(ThisKey = nameof(QuotationId), OtherKey = nameof(Quotation.Id), CanBeNull = false)]
    public Quotation Quotation { get; set; } = null!;

}
