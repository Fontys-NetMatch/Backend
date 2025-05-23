using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.Domain.Interfaces.BLL
{
    public interface ICustomerContainer
    {
        Task CreateAsync(Customer customer);
        Task<Customer?> GetByIdAsync(int id);
        Task UpdateAsync(Customer customer);
        Task<List<Customer>> GetAllAsync();
    }
}
