using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Interfaces
{
    public interface IProductImageRepository
    {
        Task<int> CreateAsync(ProductImage image);
        Task<List<ProductImage>> GetAllActiveAsync();
        Task<ProductImage?> GetByIdAsync(int id);
        Task<List<ProductImage>> GetByProductIdAsync(int productId);
        Task<int> UpdateAsync(ProductImage image);
    }
}