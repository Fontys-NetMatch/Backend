using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.BLL.Container
{
    public class ProductAddonTranslationContainer : IProductAddonTranslationContainer
    {
        private readonly DbManager _db;

        public ProductAddonTranslationContainer(DbManager db)
        {
            _db = db;
        }

        public async Task<int> CreateProductAddonTranslationAsync(ProductAddonTranslation productAddonTranslation)
        {
            if (productAddonTranslation == null)
            {
                throw new ArgumentNullException(nameof(productAddonTranslation), "ProductAddonTranslation cannot be null");
            }

            var result = await _db.InsertWithInt32IdentityAsync(productAddonTranslation);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to create ProductAddonTranslation in the database");
            }

            return result;
        }

        public async Task<ProductAddonTranslation?> GetProductAddonTranslationByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductAddonTranslation Id must be positive", nameof(id));
            }

            return await _db.ProductAddonTranslations.FirstOrDefaultAsync(pat => pat.Id == id);
        }

        public async Task UpdateProductAddonTranslationAsync(ProductAddonTranslation productAddonTranslation)
        {
            if (productAddonTranslation == null)
            {
                throw new ArgumentNullException(nameof(productAddonTranslation), "ProductAddonTranslation cannot be null");
            }

            if (productAddonTranslation.Id <= 0)
            {
                throw new ArgumentException("ProductAddonTranslation must have a valid Id");
            }

            var result = await _db.UpdateAsync(productAddonTranslation);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to update ProductAddonTranslation");
            }
        }

        public async Task<IEnumerable<ProductAddonTranslation>> GetAllTranslationsByLangIsoCodeAsync(string langIsoCode)
        {
            if (string.IsNullOrWhiteSpace(langIsoCode))
            {
                throw new ArgumentException("Language ISO code must be provided", nameof(langIsoCode));
            }

            var translations = await _db.ProductAddonTranslations
                                       .Where(pat => pat.LangIsoCode == langIsoCode)
                                       .ToListAsync();

            if (translations == null || !translations.Any())
            {
                throw new InvalidOperationException("No translations found for the given language ISO code");
            }

            return translations;
        }

        public async Task DeleteProductAddonTranslationAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductAddonTranslation Id must be positive", nameof(id));
            }

            var productAddonTranslation = await GetProductAddonTranslationByIdAsync(id);
            if (productAddonTranslation == null)
            {
                throw new InvalidOperationException("ProductAddonTranslation does not exist and cannot be deleted");
            }

            var result = await _db.DeleteAsync(productAddonTranslation);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to delete ProductAddonTranslation");
            }
        }
    }
}
