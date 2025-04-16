namespace TravelPlanner.API.Response.Error;

public record InvalidCredentialsResponse : ErrorResponse
{
    public InvalidCredentialsResponse() : base("Invalid credentials")
    {
        StatusCode = 401;
    }
}