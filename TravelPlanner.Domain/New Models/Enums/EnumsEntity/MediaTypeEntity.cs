using LinqToDB.Mapping;

namespace TravelPlanner.Domain.New_Models.Entities.Media;

[Table(Name = "MediaTypes")]
public class MediaTypeEntity
{
    [Column, PrimaryKey]
    public int Id { get; set; }

    [Column, NotNull]
    public string Name { get; set; } = null!;
}