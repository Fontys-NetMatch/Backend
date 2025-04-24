using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities.Product;

namespace TravelPlanner.BLL
{
    public class ProductAddonContainer
    {
        private readonly DbManager _db;

        public ProductAddonContainer(DbManager db)
        {
            _db = db;
        }

        public async Task<int> CreateProductAddon(ProductAddon productAddon)
        {
            if (productAddon == null)
            {
                throw new ArgumentNullException(nameof(productAddon), "ProductAddon cannot be null");
            }

            var result = await _db.InsertWithInt32IdentityAsync(productAddon);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to create ProductAddon in the database");
            }

            return result;
        }

        public async Task<ProductAddon?> GetProductAddonByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductAddon Id must be positive", nameof(id));
            }

            return await _db.ProductAddons.FirstOrDefaultAsync(pa => pa.Id == id);
        }

        public async Task UpdateProductAddon(ProductAddon productAddon)
        {
            if (productAddon == null)
            {
                throw new ArgumentNullException(nameof(productAddon), "ProductAddon cannot be null");
            }

            if (productAddon.Id <= 0)
            {
                throw new ArgumentException("ProductAddon must have a valid Id");
            }

            var result = await _db.UpdateAsync(productAddon);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to update ProductAddon");
            }
        }

        public async Task<IEnumerable<ProductAddon>> GetAllActiveProductAddonsAsync()
        {
            var productAddons = await _db.ProductAddons
                                  .Where(pa => pa.IsActive)
                                  .ToListAsync();

            if (productAddons == null || !productAddons.Any())
            {
                throw new InvalidOperationException("No active ProductAddons found");
            }

            return productAddons;
        }

        public async Task SoftDeleteProductAddon(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductAddon Id must be positive", nameof(id));
            }

            var productAddon = await GetProductAddonByIdAsync(id);
            if (productAddon == null)
            {
                throw new InvalidOperationException("ProductAddon does not exist and cannot be soft-deleted");
            }

            if (!productAddon.IsActive)
            {
                throw new InvalidOperationException("ProductAddon is already inactive");
            }

            productAddon.IsActive = false;
            await UpdateProductAddon(productAddon);
        }
    }
}
