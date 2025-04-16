namespace TravelPlanner.API.Response.Success;

public record SuccessResponse : BaseResponse
{

    public string Message { get; set; } = "Success";

    public SuccessResponse(string message)
    {
        StatusCode = 200;
        Message = message;
    }

}