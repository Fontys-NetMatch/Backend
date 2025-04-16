namespace TravelPlanner.API.Response.Error;

public record UnauthorizedResponse : ErrorResponse
{
    public UnauthorizedResponse(string message) : base(message)
    {
        StatusCode = 401;
    }
}