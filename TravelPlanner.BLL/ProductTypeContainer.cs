using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.BLL
{
    public class ProductTypeContainer
    {
        private readonly DbManager _db;

        public ProductTypeContainer(DbManager db)
        {
            _db = db;
        }

        public async Task<int> CreateProductTypeAsync(ProductType productType)
        {
            if (productType == null)
            {
                throw new ArgumentNullException(nameof(productType), "ProductType cannot be null");
            }

            var result = await _db.InsertWithInt32IdentityAsync(productType);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to create ProductType in the database");
            }

            return result;
        }

        public async Task<ProductType?> GetProductTypeByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductType Id must be positive", nameof(id));
            }

            return await _db.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == id);
        }

        public async Task UpdateProductTypeAsync(ProductType productType)
        {
            if (productType == null)
            {
                throw new ArgumentNullException(nameof(productType), "ProductType cannot be null");
            }

            if (productType.Id <= 0)
            {
                throw new ArgumentException("ProductType must have a valid Id");
            }

            var result = await _db.UpdateAsync(productType);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to update ProductType");
            }
        }

        public async Task<IEnumerable<ProductType>> GetAllActiveProductTypesAsync()
        {
            var productTypes = await _db.ProductTypes
                                  .Where(pt => pt.IsActive)
                                  .ToListAsync();

            if (productTypes == null || !productTypes.Any())
            {
                throw new InvalidOperationException("No active ProductTypes found");
            }

            return productTypes;
        }

        public async Task SoftDeleteProductTypeAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductType Id must be positive", nameof(id));
            }

            var productType = await GetProductTypeByIdAsync(id);
            if (productType == null)
            {
                throw new InvalidOperationException("ProductType does not exist and cannot be soft-deleted");
            }

            if (!productType.IsActive)
            {
                throw new InvalidOperationException("ProductType is already inactive");
            }

            productType.IsActive = false;
            await UpdateProductTypeAsync(productType);
        }
    }
}
