using LinqToDB.Mapping;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.Models.Entities.UniqueSellingPoints
{
    [Table("UniqueSellingPoints")]
    public class UniqueSellingPoint
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column, NotNull]
        public UniqueSellingPointType Type { get; set; } 
        [Column, Nullable]
        public string? Value { get; set; } 
    }
}
