using TravelPlanner.Domain.Enums;

public class QuotationFiltersData
{
    public string? Name { get; set; }
    public List<QuotationStatus>? Statuses { get; set; }
    public string? SearchQuery { get; set; }              
}
