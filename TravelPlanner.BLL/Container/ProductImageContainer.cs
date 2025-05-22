using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.BLL.Container
{
    public class ProductImageContainer(DbManager db) : IProductImageContainer
    {
        public async Task<int> CreateProductImage(ProductImage productImage)
        {
            if (productImage == null)
            {
                throw new ArgumentNullException(nameof(productImage), "ProductImage cannot be null");
            }

            var result = await db.InsertWithInt32IdentityAsync(productImage);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to create ProductImage in the database");
            }

            return result;
        }

        public async Task<ProductImage?> GetProductImageById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductImage Id must be positive", nameof(id));
            }

            return await db.ProductImages.FirstOrDefaultAsync(pi => pi.Id == id);
        }

        public async Task UpdateProductImage(ProductImage productImage)
        {
            if (productImage == null)
            {
                throw new ArgumentNullException(nameof(productImage), "ProductImage cannot be null");
            }

            if (productImage.Id <= 0)
            {
                throw new ArgumentException("ProductImage must have a valid Id", nameof(productImage));
            }

            var result = await db.UpdateAsync(productImage);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to update ProductImage");
            }
        }

        public async Task<IEnumerable<ProductImage>> GetAllActiveProductImages()
        {
            var productImages = await db.ProductImages
                                  .Where(pi => pi.DeletedAt == null)
                                  .ToListAsync();

            if (productImages is not {Count: < 1})
            {
                throw new InvalidOperationException("No active ProductImages found");
            }

            return productImages;
        }

        public async Task<bool> SoftDelete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductImage Id must be positive", nameof(id));
            }

            var productImage = await GetProductImageById(id);
            if (productImage == null)
            {
                throw new InvalidOperationException("ProductImage does not exist and cannot be soft-deleted");
            }

            if (productImage.DeletedAt == null)
            {
                throw new InvalidOperationException("ProductImage is already soft-deleted");
            }

            productImage.DeletedAt = DateTime.UtcNow;
            try
            {
                await UpdateProductImage(productImage);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new InvalidOperationException("Failed to delete product");
            }
        }

        public async Task<bool> Restore(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid product Id", nameof(id));
            }

            var product = await GetProductImageById(id);
            if (product == null)
            {
                throw new InvalidOperationException("Product does not exist");
            }

            if (product.DeletedAt == null)
            {
                throw new InvalidOperationException("Product is already restored");
            }

            product.DeletedAt = null;
            
            if (await db.UpdateAsync(product) == 0)
            {
                throw new InvalidOperationException("Failed to restore product");
            }

            return true;
        }
    }
}
