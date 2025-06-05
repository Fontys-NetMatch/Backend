using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task InsertUserAsync(User user);
    }
}