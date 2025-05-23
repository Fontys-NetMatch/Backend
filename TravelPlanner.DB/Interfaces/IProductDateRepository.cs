using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Interfaces
{
    public interface IProductDateRepository
    {
        Task<int> CreateAsync(ProductDate productDate);
        Task<List<ProductDate>> GetAllActiveAsync();
        Task<ProductDate?> GetByIdAsync(int id);
        Task<int> UpdateAsync(ProductDate productDate);
    }
}