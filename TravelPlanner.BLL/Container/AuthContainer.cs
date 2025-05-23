using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Exceptions;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Request.Auth;

namespace TravelPlanner.BLL.Container;

public class AuthContainer : IAuthContainer
{
    private readonly IAuthRepository _repository;

    public AuthContainer(IAuthRepository repository)
    {
        _repository = repository;
    }

    public User LoginUser(LoginData data)
    {
        var user = _repository.GetUserByEmailAsync(data.Email).Result;
        if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(data.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        return user;
    }

    public void RegisterUser(RegisterData data)
    {
        if (string.IsNullOrWhiteSpace(data.Firstname) ||
            string.IsNullOrWhiteSpace(data.Surname) ||
            string.IsNullOrWhiteSpace(data.Email) ||
            string.IsNullOrWhiteSpace(data.Password))
        {
            throw new BllException("Please fill in the required fields");
        }

        if (!data.Email.Contains("@") || !data.Email.Contains("."))
        {
            throw new BllException("Invalid email");
        }

        var existingUser = _repository.GetUserByEmailAsync(data.Email).Result;
        if (existingUser != null)
        {
            throw new BllException("Email already in use");
        }

        var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(data.Password);

        var newUser = new User
        {
            Firstname = data.Firstname,
            Surname = data.Surname,
            Email = data.Email,
            Password = hashedPassword,
            IsActive = true
        };

        _repository.InsertUserAsync(newUser).Wait();
    }
}