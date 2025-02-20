namespace TravelPlanner.API.Models.Response.Error;

public record ValidationErrorResponse : BaseResponse
{
    public List<string> Errors { get; set; }

    public ValidationErrorResponse(string message, List<string> errors) : base(message)
    {
        Errors = errors;
        StatusCode = 400;
    }
}