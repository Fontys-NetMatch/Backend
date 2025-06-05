using LinqToDB.Mapping;
using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Enums;

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
    public MediaType MediaType { get; set; }

    [Column, NotNull]
    public MediaSource MediaSource { get; set; }

}
