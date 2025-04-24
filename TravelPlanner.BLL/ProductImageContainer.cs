using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.BLL
{
    public class ProductImageContainer
    {
        private readonly DbManager _db;

        public ProductImageContainer(DbManager db)
        {
            _db = db;
        }

        public async Task<int> CreateProductImageAsync(ProductImage productImage)
        {
            if (productImage == null)
            {
                throw new ArgumentNullException(nameof(productImage), "ProductImage cannot be null");
            }

            var result = await _db.InsertWithInt32IdentityAsync(productImage);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to create ProductImage in the database");
            }

            return result;
        }

        public async Task<ProductImage?> GetProductImageByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductImage ID must be positive", nameof(id));
            }

            return await _db.ProductImages.FirstOrDefaultAsync(pi => pi.Id == id);
        }

        public async Task UpdateProductImageAsync(ProductImage productImage)
        {
            if (productImage == null)
            {
                throw new ArgumentNullException(nameof(productImage), "ProductImage cannot be null");
            }

            if (productImage.Id <= 0)
            {
                throw new ArgumentException("ProductImage must have a valid ID", nameof(productImage));
            }

            var result = await _db.UpdateAsync(productImage);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to update ProductImage");
            }
        }

        public async Task<IEnumerable<ProductImage>> GetAllActiveProductImagesAsync()
        {
            var productImages = await _db.ProductImages
                                  .Where(pi => pi.DeletedAt == null)
                                  .ToListAsync();

            if (productImages == null || productImages.Count == 0)
            {
                throw new InvalidOperationException("No active ProductImages found");
            }

            return productImages;
        }

        public async Task SoftDeleteProductImageAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductImage ID must be positive", nameof(id));
            }

            var productImage = await GetProductImageByIdAsync(id);
            if (productImage == null)
            {
                throw new InvalidOperationException("ProductImage does not exist and cannot be soft-deleted");
            }

            if (productImage.DeletedAt == null)
            {
                throw new InvalidOperationException("ProductImage is already soft-deleted");
            }

            productImage.DeletedAt = DateTime.UtcNow;
            await UpdateProductImageAsync(productImage);
        }
    }
}
