using System.Diagnostics;
using TravelPlanner.Domain.New_Models.Entities.Media;

namespace TravelPlanner.Domain.New_Models.Entities.Program;

public class DayProgram
{
<<<<<<< Updated upstream
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int? StartDay { get; set; }
    public int? EndDay { get; set; }
    public MediaContext? Media { get; set; }
    public GeoCoordinates? Coordinates { get; set; }
    public int? TravelDistance { get; set; }
}
=======
    [Table(Name = "DayPrograms")]
    public record DayProgram
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column, Nullable]
        public string? Title { get; set; }

        [Column, Nullable]
        public string? Content { get; set; }

        [Column, Nullable]
        public int? StartDay { get; set; }

        [Column, Nullable]
        public int? EndDay { get; set; }

        [Column, Nullable]
        public int? TravelDistance { get; set; }

        [Association(ThisKey = "Id", OtherKey = "DayProgramId")]
        public IEnumerable<MediaContext>? Media { get; set; }

        [Column, Nullable]
        public int? GeoCoordinateId { get; set; }

        [Association(ThisKey = "GeoCoordinateId", OtherKey = "Id")]
        public GeoCoordinates? GeoCoordinate { get; set; }
        
        public DayProgram(
            int id,
            string? title,
            string? content,
            int? startDay,
            int? endDay,
            int? travelDistance,
            IEnumerable<MediaContext>? media,
            int? geoCoordinateId,
            GeoCoordinates? geoCoordinate)
        {
            Id = id;
            Title = title;
            Content = content;
            StartDay = startDay;
            EndDay = endDay;
            TravelDistance = travelDistance;
            Media = media;
            GeoCoordinateId = geoCoordinateId;
            GeoCoordinate = geoCoordinate;
        }
    }
}
>>>>>>> Stashed changes
