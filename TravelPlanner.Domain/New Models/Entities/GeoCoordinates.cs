using LinqToDB.Mapping;

namespace TravelPlanner.Domain.New_Models.Entities;

[Table(Name = "GeoCoordinates")]
public class GeoCoordinates
{
    [Column, PrimaryKey, Identity]
    public int Id { get; set; }

    [Column, NotNull]
    public double Longitude { get; set; }

    [Column, NotNull]
    public double Latitude { get; set; }
}
