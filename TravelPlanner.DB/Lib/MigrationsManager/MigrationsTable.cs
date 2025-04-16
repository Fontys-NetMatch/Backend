using LinqToDB;
using LinqToDB.Mapping;

namespace TravelPlanner.DB.Lib.MigrationsManager;

[Table("Migrations")]
public class MigrationsTable
{

    [Column, PrimaryKey, Identity]
    public int Id { get; set; }

    [Column(Length = 1000), NotNull]
    public required string ClassName { get; set; }

    [Column(DataType = DataType.Int32), NotNull]
    public DateTime RunAt { get; set; }

}