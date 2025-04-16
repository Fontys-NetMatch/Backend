namespace TravelPlanner.API.Response.Success;

public record NoContentResponse : BaseResponse
{

    public NoContentResponse(string message) : base(message)
    {
        StatusCode = 204;
    }

}