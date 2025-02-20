namespace TravelPlanner.API.Response.Error;

public record ErrorResponse : BaseResponse
{
    public ErrorResponse(string message) : base(message)
    {
        StatusCode = 400;
    }
}