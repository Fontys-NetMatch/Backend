namespace TravelPlanner.API.Response.Success.ProductDate;

public class ProductDateResponse
{

    public int ID { get; set; }
    public double Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? Slots { get; set; }
    public bool IsActive { get; set; }

    public ProductDateResponse(int id, double price, DateTime startDate, DateTime endDate, int? slots, bool isActive)
    {
        ID = id;
        Price = price;
        StartDate = startDate;
        EndDate = endDate;
        Slots = slots;
        IsActive = isActive;
    }

}