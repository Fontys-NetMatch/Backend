namespace TravelPlanner.API.Response.Success.Quotation;

public record PriceCalcResponse : BaseResponse
{
    public double Price { get; set; }

    public PriceCalcResponse(double Price)
    {
        this.Price = Price;
    }
}