using TravelPlanner.Domain.Models.Entities.Product;

namespace TravelPlanner.Domain.Interfaces.BLL.Container
{
    public interface IProductAddonContainer
    {
        Task<int> CreateProductAddon(ProductAddon productAddon);
        Task<ProductAddon?> GetProductAddonByIdAsync(int id);
        Task UpdateProductAddon(ProductAddon productAddon);
        Task<IEnumerable<ProductAddon>> GetAllActiveProductAddonsAsync();
        Task SoftDeleteProductAddon(int id);
    }
}
