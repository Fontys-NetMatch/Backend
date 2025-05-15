namespace TravelPlanner.Domain.New_Models.Entities.Product;

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
    
}