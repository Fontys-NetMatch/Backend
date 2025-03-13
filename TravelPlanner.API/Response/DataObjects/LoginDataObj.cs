using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.API.Response.DataObjects;

public class LoginDataObj
{

    public string JwtToken { get; init; }

    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }

    public LoginDataObj(string jwtToken, User user)
    {
        JwtToken = jwtToken;

        Id = user.Id;
        Firstname = user.Firstname;
        Lastname = user.Lastname;
        Email = user.Email;
        Phone = user.Phone;
    }

}