using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Interfaces
{
    public interface IUserRepository
    {
        int Create(User user);
        Task<List<User>> GetAllActiveAsync();
        User? GetByEmail(string email);
        Task<User?> GetByIdAsync(int id);
        Task<int> UpdateAsync(User user);
    }
}