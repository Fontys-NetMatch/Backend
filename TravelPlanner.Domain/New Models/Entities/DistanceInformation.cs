using LinqToDB.Mapping;

namespace TravelPlanner.Domain.New_Models.Entities
{
    public class DistanceInformation
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(DataType = LinqToDB.DataType.VarChar, Length = 100), Nullable]
        public string? PointOfInterestType { get; set; }

        [Column(DataType = LinqToDB.DataType.VarChar, Length = 255), Nullable]
        public string? PointOfInterest { get; set; }

        [Column(DataType = LinqToDB.DataType.Double), Nullable]
        public double? Distance { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? Description { get; set; }

        public DistanceInformation(
            string? pointOfInterestType = null,
            string? pointOfInterest = null,
            double? distance = null,
            string? description = null)
        {
            PointOfInterestType = pointOfInterestType;
            PointOfInterest = pointOfInterest;
            Distance = distance;
            Description = description;
        }
    }
}