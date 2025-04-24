namespace TravelPlanner.API.Response.Success.Quotation;

public record PriceCalcResponse : BaseResponse
{
    public double Price;

    public PriceCalcResponse(double Price)
    {
        this.Price = Price;
    }
}