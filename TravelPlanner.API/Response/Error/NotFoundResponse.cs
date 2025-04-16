namespace TravelPlanner.API.Response.Error;

public record NotFoundResponse : ErrorResponse
{
    public NotFoundResponse(string message) : base(message)
    {
        StatusCode = 404;
    }
}