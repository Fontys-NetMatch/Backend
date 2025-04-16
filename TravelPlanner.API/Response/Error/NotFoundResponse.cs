namespace TravelPlanner.API.Response.Error;

public record NotFoundResponse : BaseResponse
{
    public NotFoundResponse(string message) : base(message)
    {
        StatusCode = 404;
    }
}