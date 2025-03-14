using LinqToDB;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities
{
    [Table("Customers")]
    public record Customer
    {
        [Column, PrimaryKey, Identity]
        public int ID { get; set; }

        [Column(Length = 255), NotNull]
        public required string Firstname { get; set; }

        [Column(Length = 255), NotNull]
        public required string Surname { get; set; }

        [Column(Length = 255), NotNull]
        public required string Email { get; set; }

        [Column(Length = 255), Nullable]
        public string? Phone { get; set; }

        [Column, NotNull]
        public required bool IsActive { get; set; }
    }
}
