using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.DB.Repositories;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.BLL.Container
{
    public class ProductImageContainer : IProductImageContainer
    {
        private readonly IProductImageRepository _repository;
        public async Task<int> CreateProductImage(ProductImage productImage)
        {
            if (productImage == null)
            {
                throw new ArgumentNullException(nameof(productImage), "ProductImage cannot be null");
            }

        public ProductImageContainer(IProductImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(ProductImage image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "ProductImage cannot be null");

            var result = await _repository.CreateAsync(image);
            if (result == 0)
                throw new InvalidOperationException("Failed to create ProductImage in the database");

            return result;
        }

        public async Task<ProductImage?> GetProductImageById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ProductImage Id must be positive", nameof(id));

            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(ProductImage image)
        public async Task UpdateProductImage(ProductImage productImage)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "ProductImage cannot be null");

            if (image.Id <= 0)
                throw new ArgumentException("ProductImage must have a valid Id", nameof(image));

            var result = await _repository.UpdateAsync(image);
            if (result == 0)
                throw new InvalidOperationException("Failed to update ProductImage");
        }

        public async Task<IEnumerable<ProductImage>> GetAllActiveProductImages()
        {
            var list = await _repository.GetAllActiveAsync();
            if (list == null || list.Count == 0)
                throw new InvalidOperationException("No active ProductImages found");

            return list;
        }

        public async Task<bool> SoftDelete(int id)
        {
            return await _repository.GetByProductIdAsync(productId);
        }

            var productImage = await GetProductImageById(id);
            if (productImage == null)
            {
                throw new InvalidOperationException("ProductImage does not exist and cannot be soft-deleted");
            }

            if (image.DeletedAt != null)
                throw new InvalidOperationException("ProductImage is already soft-deleted");

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
            var image = await GetByIdAsync(id)
                         ?? throw new InvalidOperationException("ProductImage does not exist");

            var product = await GetProductImageById(id);
            if (product == null)
            {
                throw new InvalidOperationException("Product does not exist");
            }

            image.DeletedAt = null;
            var result = await _repository.UpdateAsync(image);
            if (result == 0)
                throw new InvalidOperationException("Failed to restore ProductImage");

            return true;
        }

    }
}
