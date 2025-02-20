namespace TravelPlanner.Domain.Models.Request.Auth;

public class LoginData
{

    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool Remember { get; set; } = false;

}