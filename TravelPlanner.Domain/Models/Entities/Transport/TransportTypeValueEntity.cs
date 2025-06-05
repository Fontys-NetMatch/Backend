using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.Transport
{
    [Table("TransportTypeValues")]
    public class TransportTypeValueEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Column, NotNull]
        public string Name { get; set; } = null!;
    }
}
