using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.Domain.Interfaces.BLL
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
