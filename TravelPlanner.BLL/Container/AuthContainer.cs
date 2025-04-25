using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Exceptions;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Request.Auth;

namespace TravelPlanner.BLL.Container;

public class AuthContainer(DbManager db) : IAuthContainer
{
    public User LoginUser(LoginData data)
    {
        var user = db.Users.FirstOrDefaultAsync(u => u.Email == data.Email).Result;
        if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(data.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }
        return user;
    }

    public void RegisterUser(RegisterData data)
    {

        var user = db.Users.FirstOrDefaultAsync(u => u.Email == data.Email).Result;

        if (data.Firstname == "" || data.Surname == "" || data.Email == "" || data.Password == "")
        {
            throw new BllException("Please fill in the required fields");
        }

        if (!data.Email.Contains("@") || !data.Email.Contains("."))
        {
            throw new BllException("Invalid email");
        }
        
        if (user != null)
        {
            throw new BllException("Email already in use");
        }

        var hashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(data.Password);

        db.Insert(new User
        {
            Firstname = data.Firstname,
            Surname = data.Surname,
            Email = data.Email,
            IsActive = true,
            Password = hashPassword
        });

    }

}