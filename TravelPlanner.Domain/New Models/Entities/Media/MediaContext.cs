using LinqToDB.Mapping;
using TravelPlanner.Domain.New_Models.Entities.Program;
using TravelPlanner.Domain.New_Models.Enums;
using TravelPlanner.Domain.New_Models.Enums.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Media;
[Table(Name = "MediaContexts")]
public class MediaContext
{
    [PrimaryKey, Identity]
    public int Id { get; set; }

    [Column, Nullable]
    public string? Identifier { get; set; }

    [Column, Nullable]
    public string? Description { get; set; }

    [Column, Nullable]
    public string? Title { get; set; }

    [Column, Nullable]
    public string? Category { get; set; }

    [Column, NotNull]
    public MediaType MediaType { get; set; }

    [Column, NotNull]
    public MediaSource MediaSource { get; set; }

    
    public MediaContext(
        int id,
        string? identifier,
        string? description,
        string? title,
        string? category,
        MediaType mediaType,
        MediaSource mediaSource)
    {
        Id = id;
        Identifier = identifier;
        Description = description;
        Title = title;
        Category = category;
        MediaType = mediaType;
        MediaSource = mediaSource;
    }
    
}
