using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.DB.Repositories;
using TravelPlanner.DB.Interfaces;

namespace TravelPlanner.BLL.Container;

public class UserContainer : IUserContainer
{
    private readonly IUserRepository _repository;

    public UserContainer(IUserRepository repository)
    {
        _repository = repository;
    }

    public void CreateUser(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (string.IsNullOrWhiteSpace(user.Email) ||
            string.IsNullOrWhiteSpace(user.Firstname) ||
            string.IsNullOrWhiteSpace(user.Surname))
        {
            throw new ArgumentException("Firstname, Surname and Email are required");
        }

        var existing = _repository.GetByEmail(user.Email);
        if (existing != null)
        {
            throw new InvalidOperationException($"A user with email {user.Email} already exists.");
        }

        if (_repository.Create(user) <= 0)
            throw new InvalidOperationException("Failed to create user");
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid user ID");

        return await _repository.GetByIdAsync(id);
    }

    public async Task UpdateUser(User user)
    {
        if (user == null || user.Id <= 0)
            throw new ArgumentException("Invalid user");

        var existing = await _repository.GetByIdAsync(user.Id);
        if (existing == null)
            throw new InvalidOperationException("User does not exist");

        if (await _repository.UpdateAsync(user) == 0)
            throw new InvalidOperationException("Failed to update user");
    }

    public async Task SoftDeleteUser(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid user ID");

        var user = await _repository.GetByIdAsync(id)
                   ?? throw new InvalidOperationException("User not found");

        if (!user.IsActive)
            throw new InvalidOperationException("User already inactive");

        user.IsActive = false;

        if (await _repository.UpdateAsync(user) == 0)
            throw new InvalidOperationException("Failed to soft-delete user");
    }

    public async Task<List<User>> GetAllActiveUsersAsync()
    {
        var users = await _repository.GetAllActiveAsync();
        if (users.Count == 0)
            throw new InvalidOperationException("No active users found");

        return users;
    }
}
