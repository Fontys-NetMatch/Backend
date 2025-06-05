using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.Domain.Interfaces.BLL.Container
{
    public interface IProductAddonTranslationContainer
    {
        Task<int> CreateAsync(ProductAddonTranslation translation);
        Task DeleteAsync(int id);
        Task<ProductAddonTranslation?> GetByIdAsync(int id);
        Task<List<ProductAddonTranslation>> GetByLangIsoCodeAsync(string langIsoCode);
        Task UpdateAsync(ProductAddonTranslation translation);
    }
}