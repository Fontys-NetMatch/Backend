using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.BLL
{
    public class ProductTranslationContainer
    {
        private readonly DbManager _db;

        public ProductTranslationContainer(DbManager db)
        {
            _db = db;
        }

        public async Task<int> CreateProductTranslationAsync(ProductTranslation productTranslation)
        {
            if (productTranslation == null)
            {
                throw new ArgumentNullException(nameof(productTranslation), "ProductTranslation cannot be null");
            }

            var result = await _db.InsertWithInt32IdentityAsync(productTranslation);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to create ProductTranslation in the database");
            }

            return result;
        }

        public async Task<ProductTranslation?> GetProductTranslationByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductTranslation ID must be positive", nameof(id));
            }

            return await _db.ProductTranslations.FirstOrDefaultAsync(pt => pt.ID == id);
        }

        public async Task UpdateProductTranslationAsync(ProductTranslation productTranslation)
        {
            if (productTranslation == null)
            {
                throw new ArgumentNullException(nameof(productTranslation), "ProductTranslation cannot be null");
            }

            if (productTranslation.ID <= 0)
            {
                throw new ArgumentException("ProductTranslation must have a valid ID");
            }

            var result = await _db.UpdateAsync(productTranslation);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to update ProductTranslation");
            }
        }

        public async Task<IEnumerable<ProductTranslation>> GetAllActiveProductTranslationsAsync()
        {
            var productTranslations = await _db.ProductTranslations
                                      .Where(pt => pt.IsActive)
                                      .ToListAsync();

            if (productTranslations == null || !productTranslations.Any())
            {
                throw new InvalidOperationException("No active ProductTranslations found");
            }

            return productTranslations;
        }

        public async Task SoftDeleteProductTranslationAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductTranslation ID must be positive", nameof(id));
            }

            var productTranslation = await GetProductTranslationByIdAsync(id);
            if (productTranslation == null)
            {
                throw new InvalidOperationException("ProductTranslation does not exist and cannot be soft-deleted");
            }

            if (!productTranslation.IsActive)
            {
                throw new InvalidOperationException("ProductTranslation is already inactive");
            }

            productTranslation.IsActive = false;
            await UpdateProductTranslationAsync(productTranslation);
        }

        public async Task<IEnumerable<ProductTranslation>> GetTranslationsByProductIdAsync(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("Product ID must be positive", nameof(productId));
            }

            var translations = await _db.ProductTranslations
                                      .Where(pt => pt.Product_ID == productId)
                                      .ToListAsync();

            if (translations == null || !translations.Any())
            {
                throw new InvalidOperationException("No translations found for the given Product ID");
            }

            return translations;
        }
    }
}
