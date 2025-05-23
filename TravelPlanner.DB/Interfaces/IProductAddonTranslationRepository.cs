using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.DB.Interfaces
{
    public interface IProductAddonTranslationRepository
    {
        Task<int> CreateAsync(ProductAddonTranslation translation);
        Task<int> DeleteAsync(ProductAddonTranslation translation);
        Task<ProductAddonTranslation?> GetByIdAsync(int id);
        Task<List<ProductAddonTranslation>> GetByLangIsoCodeAsync(string langIsoCode);
        Task<int> UpdateAsync(ProductAddonTranslation translation);
    }
}