namespace TravelPlanner.Domain.Models.Request.Auth;

public class RegisterData
{

    public required string Firstname { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required string Password { get; set; }
    public required bool IsActive { get; set; }

}