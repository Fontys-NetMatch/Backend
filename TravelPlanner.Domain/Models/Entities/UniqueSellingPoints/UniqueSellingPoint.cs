using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.UniqueSellingPoints
{
    [Table("UniqueSellingPoints")]
    public class UniqueSellingPoint
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column, NotNull]
        public string Type { get; set; } = null!; // ✅ enum as string

        [Column, Nullable]
        public string? Value { get; set; } // Tekst van de USP (optioneel)

        [Column, NotNull]
        public int ProductId { get; set; } // FK naar Product
    }
}
