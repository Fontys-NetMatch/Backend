using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Interfaces
{
    public interface IProductTypeRepository
    {
        Task<int> CreateAsync(ProductTypeEntity productType);
        Task<List<ProductTypeEntity>> GetAllActiveAsync();
        Task<List<ProductTypeEntity>> GetAllAsync();
        Task<ProductTypeEntity?> GetByIdAsync(int id);
        Task<int> UpdateAsync(ProductTypeEntity productType);
    }
}