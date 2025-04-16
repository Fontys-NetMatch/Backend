namespace TravelPlanner.API.Response.Success;

public record NoContentResponse : BaseResponse
{

    public NoContentResponse()
    {
        StatusCode = 204;
    }

}