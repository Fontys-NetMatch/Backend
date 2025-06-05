using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.Domain.Interfaces.BLL.Container;

public interface IProductAddonTranslationContainer
{
    Task<int> CreateProductAddonTranslationAsync(ProductAddonTranslation productAddonTranslation);
    Task<ProductAddonTranslation?> GetProductAddonTranslationByIdAsync(int id);
    Task UpdateProductAddonTranslationAsync(ProductAddonTranslation productAddonTranslation);
    Task<IEnumerable<ProductAddonTranslation>> GetAllTranslationsByLangIsoCodeAsync(string langIsoCode);
    Task DeleteProductAddonTranslationAsync(int id);
}