using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.BLL
{
    public class ProductTypeTranslationContainer
    {
        private readonly DbManager _db;

        public ProductTypeTranslationContainer(DbManager db)
        {
            _db = db;
        }

        public async Task<int> CreateProductTypeTranslationAsync(ProductTypeTranslation productTypeTranslation)
        {
            if (productTypeTranslation == null)
            {
                throw new ArgumentNullException(nameof(productTypeTranslation), "ProductTypeTranslation cannot be null");
            }

            var result = await _db.InsertWithInt32IdentityAsync(productTypeTranslation);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to create ProductTypeTranslation in the database");
            }

            return result;
        }

        public async Task<ProductTypeTranslation?> GetProductTypeTranslationByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductTypeTranslation ID must be positive", nameof(id));
            }

            return await _db.ProductTypeTranslations.FirstOrDefaultAsync(ptt => ptt.ID == id);
        }

        public async Task UpdateProductTypeTranslationAsync(ProductTypeTranslation productTypeTranslation)
        {
            if (productTypeTranslation == null)
            {
                throw new ArgumentNullException(nameof(productTypeTranslation), "ProductTypeTranslation cannot be null");
            }

            if (productTypeTranslation.ID <= 0)
            {
                throw new ArgumentException("ProductTypeTranslation must have a valid ID");
            }

            var result = await _db.UpdateAsync(productTypeTranslation);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to update ProductTypeTranslation");
            }
        }

        public async Task<IEnumerable<ProductTypeTranslation>> GetTranslationsByLangIsoCodeAsync(string langIsoCode)
        {
            if (string.IsNullOrWhiteSpace(langIsoCode))
            {
                throw new ArgumentException("Language ISO code must be provided", nameof(langIsoCode));
            }

            var translations = await _db.ProductTypeTranslations
                                       .Where(ptt => ptt.LangIsoCode == langIsoCode)
                                       .ToListAsync();

            if (translations == null || !translations.Any())
            {
                throw new InvalidOperationException("No translations found for the given language ISO code");
            }

            return translations;
        }

        public async Task SoftDeleteProductTypeTranslationAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductTypeTranslation ID must be positive", nameof(id));
            }

            var productTypeTranslation = await GetProductTypeTranslationByIdAsync(id);
            if (productTypeTranslation == null)
            {
                throw new InvalidOperationException("ProductTypeTranslation does not exist and cannot be soft-deleted");
            }

            productTypeTranslation.Name = string.Empty; // Example: Clearing the name field
            await UpdateProductTypeTranslationAsync(productTypeTranslation);
        }
    }
}
