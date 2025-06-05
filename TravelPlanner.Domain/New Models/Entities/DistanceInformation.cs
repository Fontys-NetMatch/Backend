namespace TravelPlanner.Domain.New_Models.Entities;

public class DistanceInformation
{


    public string? PointOfInterestType { get; set; }
    public string? PointOfInterest { get; set; }
    public double? Distance { get; set; }
    public string? Description { get; set; }
    
    public DistanceInformation(string? pointOfInterestType = null, string? pointOfInterest = null, double? distance = default, string? description = null)
    {
<<<<<<< Updated upstream
        PointOfInterestType = pointOfInterestType;
        PointOfInterest = pointOfInterest;
        Distance = distance;
        Description = description;
=======
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
            string? pointOfInterestType,
            string? pointOfInterest,
            double? distance,
            string? description)
        {
            PointOfInterestType = pointOfInterestType;
            PointOfInterest = pointOfInterest;
            Distance = distance;
            Description = description;
        }
>>>>>>> Stashed changes
    }
}