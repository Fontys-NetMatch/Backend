using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Request.Product;

namespace TravelPlanner.DB.Interfaces
{
    public interface IProductRepository
    {
        Task<int> CreateAsync(Product product);
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> GetFilteredAsync(ProductFiltersData filters);
        Task<int> UpdateAsync(Product product);
    }
}