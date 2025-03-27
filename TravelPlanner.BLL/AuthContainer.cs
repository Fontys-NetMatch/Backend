using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Exceptions;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Request.Auth;

namespace TravelPlanner.BLL;

public class AuthContainer : IAuthContainer
{
    // Private field to interact with the database
    private readonly DbManager _db;

    // Constructor to initialize the DbManager
    public AuthContainer(DbManager db)
    {
        _db = db;
    }

    // Method to handle user login
    public User LoginUser(LoginData data)
    {
        // Find the user by email in the database
        var user = _db.Users.FirstOrDefaultAsync(u => u.Email == data.Email).Result;

        // If no user is found, throw invalid credentials exception
        if (user == null)
        {
            throw new InvalidCredentialsException();
        }

        // Check if the provided password matches the stored hashed password
        if (!BCrypt.Net.BCrypt.EnhancedVerify(data.Password, user.Password))
        {
            throw new InvalidCredentialsException(); // Throw if passwords don't match
        }

        // Return the user object if login is successful
        return user;
    }

    // Method to handle user registration
    public void RegisterUser(RegisterData data)
    {
        // Check if the email is already in use
        var user = _db.Users.FirstOrDefaultAsync(u => u.Email == data.Email).Result;

        if (user != null)
        {
            throw new BllException("Email already in use"); // Throw if email exists
        }

        // Hash the password before saving it
        var hashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(data.Password);

        // Insert new user into the database
        _db.Insert(new User
        {
            Firstname = data.Firstname,
            Surname = data.Surname,
            Email = data.Email,
            IsActive = data.IsActive,
            Password = hashPassword // Save the hashed password
        });
    }
}
