using TravelPlanner.API.Response.DataObjects;

namespace TravelPlanner.API.Response.Success;

public record LoginResponse : BaseResponse
{

    public new LoginDataObj Data { get; init; }

    public LoginResponse(string message, LoginDataObj login) : base(message)
    {
        StatusCode = 200;
        Data = login;
    }

}