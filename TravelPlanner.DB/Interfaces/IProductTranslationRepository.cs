using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.DB.Interfaces
{
    public interface IProductTranslationRepository
    {
        Task<int> CreateAsync(ProductTranslation translation);
        Task<int> DeleteAsync(ProductTranslation translation);
        Task<List<ProductTranslation>> GetAllActiveAsync(string isoCode);
        Task<List<ProductTranslation>> GetAllAsync(string isoCode);
        Task<ProductTranslation?> GetByIdAndIsoAsync(int id, string isoCode);
        Task<ProductTranslation?> GetByIdAsync(int id);
        Task<int> UpdateAsync(ProductTranslation translation);
    }
}