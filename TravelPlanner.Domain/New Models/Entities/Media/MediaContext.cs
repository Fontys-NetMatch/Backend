using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Media;

public class MediaContext
{


    public string? Identifier { get; set; }
    public string? Description { get; set; }
    public string? Title { get; set; }
    public string? Category { get; set; }
    public Mediatype MediaType { get; set; }
    public MediaSource MediaSource { get; set; }
    
    public MediaContext(string? identifier = null, string? description = null, string? title = null, string? category = null, Mediatype mediaType = default, MediaSource mediaSource = default)
    {
        Identifier = identifier;
        Description = description;
        Title = title;
        Category = category;
        MediaType = mediaType;
        MediaSource = mediaSource;
    }
}