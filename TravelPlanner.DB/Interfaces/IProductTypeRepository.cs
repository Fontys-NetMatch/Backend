using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Interfaces
{
    public interface IProductTypeRepository
    {
        Task<int> CreateAsync(ProductType productType);
        Task<List<ProductType>> GetAllActiveAsync();
        Task<List<ProductType>> GetAllAsync();
        Task<ProductType?> GetByIdAsync(int id);
        Task<int> UpdateAsync(ProductType productType);
    }
}