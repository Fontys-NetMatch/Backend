using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.BLL;

public class UserContainer
{
    private readonly DbManager _db;

    // Constructor to initialize DbManager
    public UserContainer(DbManager db)
    {
        _db = db;
    }

    // Method to create a new user
    public void CreateUser(User user)
    {
        // Check if the user is null
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null");
        }

        // Check if essential user data is missing
        if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Firstname) || string.IsNullOrEmpty(user.Surname))
        {
            throw new ArgumentException("Essential user data (Email, Firstname, or Surname) is missing");
        }

        // Check if a user with the same email already exists
        var existingUser = _db.Users.FirstOrDefault(u => u.Email == user.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException($"A user with the email {user.Email} already exists.");
        }

        // Insert the new user into the database
        var result = _db.InsertWithInt32Identity(user);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to create user in the database");
        }
    }

    // Method to get a user by their ID
    public async Task<User?> GetUserByIdAsync(int id)
    {
        // Check if the ID is valid
        if (id <= 0)
        {
            throw new ArgumentException("User ID must be positive", nameof(id));
        }

        // Retrieve the user with the given ID
        return await _db.Users.FirstOrDefaultAsync(u => u.ID == id);
    }

    // Method to update an existing user
    public async Task UpdateUser(User user)
    {
        // Check if the user is null
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null");
        }

        // Check if the user has a valid ID
        if (user.ID <= 0)
        {
            throw new ArgumentException("User must have a valid ID");
        }

        // Check if the user exists before updating
        var existingUser = await GetUserByIdAsync(user.ID);
        if (existingUser == null)
        {
            throw new InvalidOperationException("User does not exist and cannot be updated");
        }

        // Update the user in the database
        var result = await _db.UpdateAsync(user);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to update user");
        }
    }

    // Method to soft delete a user (deactivate the user)
    public async Task SoftDeleteUser(int id)
    {
        // Check if the ID is valid
        if (id <= 0)
        {
            throw new ArgumentException("User ID must be positive", nameof(id));
        }

        // Retrieve the user by ID
        var user = await GetUserByIdAsync(id);
        if (user == null)
        {
            throw new InvalidOperationException("User does not exist and cannot be soft-deleted");
        }

        // Check if the user is already inactive
        if (!user.IsActive)
        {
            throw new InvalidOperationException("User is already inactive");
        }

        // Set the user as inactive and update the database
        user.IsActive = false;
        await UpdateUser(user);
    }

    // Method to get all active users
    public async Task<IEnumerable<User>> GetAllActiveUsersAsync()
    {
        // Retrieve all active users from the database
        var users = await _db.Users
                        .Where(u => u.IsActive)
                        .ToListAsync();

        // Check if no active users are found
        if (users == null || !users.Any())
        {
            throw new InvalidOperationException("No active users found");
        }

        return users;
    }
}
