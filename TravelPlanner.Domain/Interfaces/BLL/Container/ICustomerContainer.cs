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
        Task CreateCustomer(Customer customer);
        Task<Customer?> GetCustomerById(int id);
        Task UpdateCustomer(Customer customer);
        Task<List<Customer>> GetAllCustomers();
    }
}
