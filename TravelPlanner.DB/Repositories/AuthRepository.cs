using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DbManager _db;

        public AuthRepository(DbManager db)
        {
            _db = db;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task InsertUserAsync(User user)
        {
            await _db.InsertAsync(user);
        }
    }
}
