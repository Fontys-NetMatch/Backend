using LinqToDB.Mapping;

namespace TravelPlanner.DB.Lib.MigrationsManager;

[Table("Migrations")]
public class MigrationsTable
{

    [Column, PrimaryKey, Identity]
    public int Id { get; set; }

    [Column(Length = 1000), NotNull]
    public required string ClassName { get; set; }

    [Column, NotNull]
    public DateTime RunAt { get; set; }

}