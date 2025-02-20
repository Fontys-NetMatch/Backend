namespace TravelPlanner.API.Models.Response.Error;

public record UnauthorizedResponse : BaseResponse
{
    public UnauthorizedResponse(string message) : base(message)
    {
        StatusCode = 401;
    }
}