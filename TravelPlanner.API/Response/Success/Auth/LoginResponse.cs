using TravelPlanner.API.Response.DataObjects;

namespace TravelPlanner.API.Response.Success.Auth;

public record LoginResponse : BaseResponse
{

    public new LoginDataObj Data { get; init; }

    public LoginResponse(LoginDataObj login)
    {
        StatusCode = 200;
        Data = login;
    }

}