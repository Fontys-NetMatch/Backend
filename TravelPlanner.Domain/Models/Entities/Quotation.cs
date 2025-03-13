using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities
{
    [Table("Quotations")]
    public record Quotation
    {
        [Column, PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Length = 255), NotNull]
        public required string Name { get; set; }

        [Column, NotNull]
        public required bool IsActive { get; set; }

        [Column, NotNull]
        public required int CustomerId { get; set; }

        [Association(ThisKey = nameof(CustomerId), OtherKey = nameof(Customer.Id))]
        public required Customer Customer { get; set; }
    }
}
