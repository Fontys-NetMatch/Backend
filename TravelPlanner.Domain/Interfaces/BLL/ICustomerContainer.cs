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
        void CreateCustomer(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task UpdateCustomer(Customer customer);
        Task SoftDeleteCustomer(int id);
        Task<List<Customer>> GetAllActiveCustomersAsync();
    }
}
