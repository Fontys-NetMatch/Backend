using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.Domain.Interfaces.BLL.Container
{
    public interface IUserContainer
    {
        void CreateUser(User user);
        Task<List<User>> GetAllActiveUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task SoftDeleteUser(int id);
        Task UpdateUser(User user);
    }
}