using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.Domain.Interfaces.BLL.Container;

public interface IProductAddonTranslationContainer
{
    Task<int> CreateAsync(ProductAddonTranslation translation);
    Task<ProductAddonTranslation?> GetByIdAsync(int id);
    Task UpdateAsync(ProductAddonTranslation translation);
    Task<List<ProductAddonTranslation>> GetByLangIsoCodeAsync(string langIsoCode);
    Task DeleteAsync(int id);
}