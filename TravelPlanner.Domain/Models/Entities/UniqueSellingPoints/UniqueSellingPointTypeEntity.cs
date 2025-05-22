using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.UniqueSellingPoints
{
    [Table("UniqueSellingPointTypes")]
    public class UniqueSellingPointTypeEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Column, NotNull]
        public string Name { get; set; } = null!;
    }
}
