namespace TravelPlanner.API.Response.Error;

public record InvalidCredentialsResponse : BaseResponse
{
    public InvalidCredentialsResponse() : base("Invalid credentials")
    {
        StatusCode = 401;
    }
}