using LinqToDB.Mapping;
using System.Collections.Generic;
using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Entities;

namespace TravelPlanner.Domain.Models.Entities
{
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

    }
}
