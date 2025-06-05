using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities
{
    [Table("ExtraOption")]
    public class ExtraOption
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(DataType = LinqToDB.DataType.VarChar, Length = 100), Nullable]
        public string? Code { get; set; }

        [Column(DataType = LinqToDB.DataType.VarChar, Length = 255), Nullable]
        public string? Name { get; set; }
    }
}
