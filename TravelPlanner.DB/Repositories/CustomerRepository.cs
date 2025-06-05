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
    public class CustomerRepository(DbManager db) : ICustomerRepository
    {
        public int Create(Customer customer)
        {
            return db.InsertWithInt32Identity(customer);
        }

        public Customer? GetByEmail(string email)
        {
            return db.Customers.FirstOrDefault(c => c.Email == email);
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await db.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> UpdateAsync(Customer customer)
        {
            return await db.UpdateAsync(customer);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await db.Customers.ToListAsync();
        }
    }
}
