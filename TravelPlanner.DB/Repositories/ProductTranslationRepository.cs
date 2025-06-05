using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.DB.Repositories
{
    public class ProductTranslationRepository : IProductTranslationRepository
    {
        private readonly DbManager _db;

        public ProductTranslationRepository(DbManager db)
        {
            _db = db;
        }

        public Task<ProductTranslation?> GetByIdAsync(int id)
        {
            return _db.ProductTranslations.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<ProductTranslation?> GetByIdAndIsoAsync(int id, string isoCode)
        {
            return _db.ProductTranslations
                .FirstOrDefaultAsync(p => p.Id == id && p.LangIsoCode == isoCode);
        }

        public Task<List<ProductTranslation>> GetAllAsync(string isoCode)
        {
            return _db.ProductTranslations
                .Where(p => p.LangIsoCode == isoCode)
                .ToListAsync();
        }

        public Task<List<ProductTranslation>> GetAllActiveAsync(string isoCode)
        {
            return _db.ProductTranslations
                .Where(p => p.IsActive && p.LangIsoCode == isoCode)
                .ToListAsync();
        }

        public Task<int> CreateAsync(ProductTranslation translation)
        {
            return _db.InsertWithInt32IdentityAsync(translation);
        }

        public Task<int> UpdateAsync(ProductTranslation translation)
        {
            return _db.UpdateAsync(translation);
        }

        public Task<int> DeleteAsync(ProductTranslation translation)
        {
            return _db.DeleteAsync(translation);
        }
    }
}
