using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.DB.Repositories
{
    public class ProductAddonTranslationRepository : IProductAddonTranslationRepository
    {
        private readonly DbManager _db;

        public ProductAddonTranslationRepository(DbManager db)
        {
            _db = db;
        }

        public Task<int> CreateAsync(ProductAddonTranslation translation)
        {
            return _db.InsertWithInt32IdentityAsync(translation);
        }

        public Task<ProductAddonTranslation?> GetByIdAsync(int id)
        {
            return _db.ProductAddonTranslations.FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<int> UpdateAsync(ProductAddonTranslation translation)
        {
            return _db.UpdateAsync(translation);
        }

        public Task<List<ProductAddonTranslation>> GetByLangIsoCodeAsync(string langIsoCode)
        {
            return _db.ProductAddonTranslations
                      .Where(t => t.LangIsoCode == langIsoCode)
                      .ToListAsync();
        }

        public Task<int> DeleteAsync(ProductAddonTranslation translation)
        {
            return _db.DeleteAsync(translation);
        }
    }
}
