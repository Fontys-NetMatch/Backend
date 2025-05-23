using TravelPlanner.Domain.Models.Entities.Product;

namespace TravelPlanner.DB.Interfaces
{
    public interface IProductAddonRepository
    {
        Task<int> CreateAsync(ProductAddon productAddon);
        Task<List<ProductAddon>> GetAllActiveAsync();
        Task<ProductAddon?> GetByIdAsync(int id);
        Task<int> UpdateAsync(ProductAddon productAddon);
    }
}