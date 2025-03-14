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

    public UserContainer(DbManager db)
    {
        _db = db;
    }

    public void CreateUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null");
        }

        if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Firstname) || string.IsNullOrEmpty(user.Surname))
        {
            throw new ArgumentException("Essential user data (Email, Firstname, or Surname) is missing");
        }

        var existingUser = _db.Users.FirstOrDefault(u => u.Email == user.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException($"A user with the email {user.Email} already exists.");
        }

        var result = _db.InsertWithInt32Identity(user);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to create user in the database");
        }
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("User ID must be positive", nameof(id));
        }

        return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task UpdateUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null");
        }

        if (user.Id <= 0)
        {
            throw new ArgumentException("User must have a valid ID");
        }

        var existingUser = await GetUserByIdAsync(user.Id);
        if (existingUser == null)
        {
            throw new InvalidOperationException("User does not exist and cannot be updated");
        }

        var result = await _db.UpdateAsync(user);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to update user");
        }
    }

    public async Task SoftDeleteUser(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("User ID must be positive", nameof(id));
        }

        var user = await GetUserByIdAsync(id);
        if (user == null)
        {
            throw new InvalidOperationException("User does not exist and cannot be soft-deleted");
        }

        if (!user.IsActive)
        {
            throw new InvalidOperationException("User is already inactive");
        }

        user.IsActive = false;
        await UpdateUser(user);
        
    }

    public async Task<IEnumerable<User>> GetAllActiveUsersAsync()
    {
        var users = await _db.Users
                        .Where(u => u.IsActive)
                        .ToListAsync();
        if (users == null || !users.Any())
        {
            throw new InvalidOperationException("No active users found");
        }
        return users;
    }
}
