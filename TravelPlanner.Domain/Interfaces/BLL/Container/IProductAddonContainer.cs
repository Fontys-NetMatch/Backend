using TravelPlanner.Domain.Models.Entities.Product;

namespace TravelPlanner.Domain.Interfaces.BLL.Container
{
    public interface IProductAddonContainer
    {
        Task<ProductAddon?> GetByIdAsync(int id);
        Task<int> CreateAsync(ProductAddon addon);
        Task UpdateAsync(ProductAddon addon);
        Task<List<ProductAddon>> GetAllActiveAsync();
        Task SoftDeleteAsync(int id);
    }
}
