using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbManager _db;

    public UserRepository(DbManager db)
    {
        _db = db;
    }

    public int Create(User user)
    {
        return _db.InsertWithInt32Identity(user);
    }

    public User? GetByEmail(string email)
    {
        return _db.Users.FirstOrDefault(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<int> UpdateAsync(User user)
    {
        return await _db.UpdateAsync(user);
    }

    public async Task<List<User>> GetAllActiveAsync()
    {
        return await _db.Users
            .Where(u => u.IsActive)
            .ToListAsync();
    }
}
