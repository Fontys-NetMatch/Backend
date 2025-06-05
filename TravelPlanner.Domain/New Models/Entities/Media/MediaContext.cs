using LinqToDB.Mapping;
using TravelPlanner.Domain.New_Models.Entities.Media;

namespace TravelPlanner.Domain.Models.Entities;

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
}
