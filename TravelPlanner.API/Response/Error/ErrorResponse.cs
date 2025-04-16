namespace TravelPlanner.API.Response.Error;

public record ErrorResponse : BaseResponse
{

    public string Message { get; set; } = string.Empty;

    public ErrorResponse(string message)
    {
        StatusCode = 400;
        Message = message;
    }
}