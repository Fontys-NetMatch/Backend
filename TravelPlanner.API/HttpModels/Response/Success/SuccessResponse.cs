namespace TravelPlanner.API.Models.Response.Success;

public record SuccessResponse : BaseResponse
{
    public SuccessResponse(string message) : base(message)
    {
        StatusCode = 200;
    }
}