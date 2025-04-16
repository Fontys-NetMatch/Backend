namespace TravelPlanner.Domain.Models.Request.Product;

public class ProductFiltersData
{

    public bool? IsDeleted { get; set; }
    public int? TypeId { get; set; }
    public string? SearchQuery { get; set; }
    public string? StartLocation { get; set; }
    public string? EndLocation { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinPeople { get; set; }

}