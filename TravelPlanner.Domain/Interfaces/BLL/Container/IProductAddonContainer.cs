using TravelPlanner.Domain.Models.Entities.Product;

namespace TravelPlanner.Domain.Interfaces.BLL.Container
{
    public interface IProductAddonContainer
    {
        Task<int> CreateAsync(ProductAddon addon);
        Task<List<ProductAddon>> GetAllActiveAsync();
        Task<ProductAddon?> GetByIdAsync(int id);
        Task SoftDeleteAsync(int id);
        Task UpdateAsync(ProductAddon addon);

    }
}
