using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Exceptions;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Request.Auth;

namespace TravelPlanner.BLL;

public class AuthContainer: IAuthContainer
{

    private readonly DbManager _db;

    public AuthContainer(DbManager db)
    {
        _db = db;
    }

    public User LoginUser(LoginData data)
    {
        var user = _db.Users.FirstOrDefaultAsync(u => u.Email == data.Email).Result;
        if (user == null)
        {
            throw new InvalidCredentialsException();
        }
        if (!BCrypt.Net.BCrypt.EnhancedVerify(data.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }
        return user;
    }

    public void RegisterUser(RegisterData data)
    {
        var user = _db.Users.FirstOrDefaultAsync(u => u.Email == data.Email).Result;

        if (user != null)
        {
            throw new BllException("Email already in use");
        }

        var hashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(data.Password);
        _db.Insert(new User
        {
            Firstname = data.Firstname,
            Lastname = data.Lastname,
            Email = data.Email,
            Password = hashPassword
        });
    }

}