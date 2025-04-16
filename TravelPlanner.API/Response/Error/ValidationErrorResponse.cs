namespace TravelPlanner.API.Response.Error;

public record ValidationErrorResponse : ErrorResponse
{
    public List<string> Errors { get; set; }

    public ValidationErrorResponse(string message, List<string> errors) : base(message)
    {
        Errors = errors;
        StatusCode = 400;
    }
}