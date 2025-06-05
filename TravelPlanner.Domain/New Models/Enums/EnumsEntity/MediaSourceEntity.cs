using LinqToDB.Mapping;

namespace TravelPlanner.Domain.New_Models.Entities.Media;

[Table(Name = "MediaSources")]
public class MediaSourceEntity
{
    [Column, PrimaryKey]
    public int Id { get; set; }

    [Column, NotNull]
    public string Name { get; set; } = null!;
    
    public MediaSourceEntity(int id, string name)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}