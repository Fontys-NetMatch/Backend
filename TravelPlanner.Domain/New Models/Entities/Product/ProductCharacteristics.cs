/*namespace TravelPlanner.Domain.New_Models.Entities.Product;

public class ProductCharacteristics
{

    public int? NumberOfBedrooms { get; set; }
    public int? NumberOfBathrooms { get; set; }
    public double? Size { get; set; }
    public string? CheckInFrom { get; set; }
    public string? CheckInUntil { get; set; }
    public string? CheckOut { get; set; }
    public int? NumberOfPets { get; set; }
    public bool? EventsAllowed { get; set; }
    public int? MinimumAge { get; set; }
    public int? MaxBabyBeds { get; set; }
    
    public ProductCharacteristics(int? numberOfBedrooms = default, int? numberOfBathrooms = default, double? size = default, string? checkInFrom = null, string? checkInUntil = null, string? checkOut = null, int? numberOfPets = default, bool? eventsAllowed = default, int? minimumAge = default, int? maxBabyBeds = default)
    {
        NumberOfBedrooms = numberOfBedrooms;
        NumberOfBathrooms = numberOfBathrooms;
        Size = size;
        CheckInFrom = checkInFrom;
        CheckInUntil = checkInUntil;
        CheckOut = checkOut;
        NumberOfPets = numberOfPets;
        EventsAllowed = eventsAllowed;
        MinimumAge = minimumAge;
        MaxBabyBeds = maxBabyBeds;
    }
    
}*/


using LinqToDB.Mapping;

namespace TravelPlanner.Domain.New_Models.Entities.Product
{
    [Table("ProductCharacteristics")]
    public class ProductCharacteristics
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column, Nullable]
        public int? NumberOfBedrooms { get; set; }

        [Column, Nullable]
        public int? NumberOfBathrooms { get; set; }

        [Column, Nullable]
        public double? Size { get; set; }

        [Column, Nullable]
        public string? CheckInFrom { get; set; }

        [Column, Nullable]
        public string? CheckInUntil { get; set; }

        [Column, Nullable]
        public string? CheckOut { get; set; }

        [Column, Nullable]
        public int? NumberOfPets { get; set; }

        [Column, Nullable]
        public bool? EventsAllowed { get; set; }

        [Column, Nullable]
        public int? MinimumAge { get; set; }

        [Column, Nullable]
        public int? MaxBabyBeds { get; set; }
    }
}
