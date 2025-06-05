using LinqToDB.Mapping;
using TravelPlanner.Domain.New_Models.Entities.Program;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Media;

public class MediaContext
{
    [Column, PrimaryKey, Identity]
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
    public int MediaTypeId { get; set; }

    [Association(ThisKey = nameof(MediaTypeId), OtherKey = nameof(MediaTypeEntity.Id), CanBeNull = false)]
    public MediaTypeEntity MediaType { get; set; } = null!;

    [Column, NotNull]
    public int MediaSourceId { get; set; }

    [Association(ThisKey = nameof(MediaSourceId), OtherKey = nameof(MediaSourceEntity.Id), CanBeNull = false)]
    public MediaSourceEntity MediaSource { get; set; } = null!;

    [Column, Nullable]
    public int DayProgramId { get; set; }

    [Association(ThisKey = nameof(DayProgramId), OtherKey = nameof(DayProgram.Id), CanBeNull = true)]
    public DayProgram DayProgram { get; set; } = null!;

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
    {
        Identifier = identifier;
        Description = description;
        Title = title;
        Category = category;
        MediaTypeId = mediaTypeId;
        MediaType = mediaType ?? throw new ArgumentNullException(nameof(mediaType));
        MediaSourceId = mediaSourceId;
        MediaSource = mediaSource ?? throw new ArgumentNullException(nameof(mediaSource));
        DayProgramId = dayProgramId;
        DayProgram = dayProgram ?? throw new ArgumentNullException(nameof(dayProgram));
    }
}
