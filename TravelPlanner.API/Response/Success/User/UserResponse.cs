using TravelPlanner.API.Response;
using TravelPlanner.Domain.Models.Entities;

public record UserResponse : BaseResponse
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string? ProfileImagePath { get; set; }
    public bool IsActive { get; set; }

    public UserResponse(int id, string firstname, string surname, string email, string profileImagePath, bool isActive)
    {
        Id = id;
        Firstname = firstname;
        Surname = surname;
        Email = email;
        ProfileImagePath = profileImagePath;
        IsActive = isActive;
    }
    public UserResponse(int id, string firstname, string surname, string email, bool isActive)
    {
        Id = id;
        Firstname = firstname;
        Surname = surname;
        Email = email;
        IsActive = isActive;
    }
}
