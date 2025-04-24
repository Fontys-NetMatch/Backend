using TravelPlanner.Domain.Enums;

namespace TravelPlanner.API.Response.Success.User
{
    public record UserResponse : BaseResponse  
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string ProfileImagePath { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }

        public UserResponse(int id, string firstName, string surname, string email, string profileImagePath, bool isActive, string password)
        {
            Id = id;
            FirstName = firstName;
            SurName = surname;
            Email = email;
            ProfileImagePath = profileImagePath;
            IsActive = isActive;
            Password = password;
        }
    }
}
