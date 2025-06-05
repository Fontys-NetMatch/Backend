using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Interfaces
{
    public interface ICustomerRepository
    {
        int Create(Customer customer);
        Task<List<Customer>> GetAllAsync();
        Customer? GetByEmail(string email);
        Task<Customer?> GetByIdAsync(int id);
        Task<int> UpdateAsync(Customer customer);
    }
}