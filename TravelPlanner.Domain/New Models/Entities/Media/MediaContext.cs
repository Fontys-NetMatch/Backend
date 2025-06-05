using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Media;

public class MediaContext
{


    public string? Identifier { get; set; }
    public string? Description { get; set; }
    public string? Title { get; set; }
    public string? Category { get; set; }
<<<<<<< Updated upstream
    public Mediatype MediaType { get; set; }
    public MediaSource MediaSource { get; set; }
    
    public MediaContext(string? identifier = null, string? description = null, string? title = null, string? category = null, Mediatype mediaType = default, MediaSource mediaSource = default)
=======

    [Column, NotNull]
    public int MediaTypeId { get; set; }

    [Association(ThisKey = nameof(MediaTypeId), OtherKey = nameof(MediaTypeEntity.Id), CanBeNull = false)]
    public MediaTypeEntity MediaType { get; set; } = null!;

    [Column, NotNull]
    public int MediaSourceId { get; set; }
    
    
    public MediaContext(
        string? identifier,
        string? description,
        string? title,
        string? category,
        int mediaTypeId,
        MediaTypeEntity mediaType,
        int mediaSourceId,
        MediaSourceEntity mediaSource,
        int dayProgramId,
        DayProgram dayProgram)
>>>>>>> Stashed changes
    {
        Identifier = identifier;
        Description = description;
        Title = title;
        Category = category;
<<<<<<< Updated upstream
        MediaType = mediaType;
        MediaSource = mediaSource;
    }
}
=======
        MediaTypeId = mediaTypeId;
        MediaType = mediaType ?? throw new ArgumentNullException(nameof(mediaType));
        MediaSourceId = mediaSourceId;
        MediaSource = mediaSource ?? throw new ArgumentNullException(nameof(mediaSource));
        DayProgramId = dayProgramId;
        DayProgram = dayProgram ?? throw new ArgumentNullException(nameof(dayProgram));
    }
}
>>>>>>> Stashed changes
