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

        public async Task<ProductImage?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ProductImage Id must be positive", nameof(id));

            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(ProductImage image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "ProductImage cannot be null");

            if (image.Id <= 0)
                throw new ArgumentException("ProductImage must have a valid Id", nameof(image));

            var result = await _repository.UpdateAsync(image);
            if (result == 0)
                throw new InvalidOperationException("Failed to update ProductImage");
        }

        public async Task<List<ProductImage>> GetAllActiveAsync()
        {
            var list = await _repository.GetAllActiveAsync();
            if (list == null || list.Count == 0)
                throw new InvalidOperationException("No active ProductImages found");

            return list;
        }

        public async Task<List<ProductImage>> GetImagesByProductIdAsync(int productId)
        {
            return await _repository.GetByProductIdAsync(productId);
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var image = await GetByIdAsync(id)
                         ?? throw new InvalidOperationException("ProductImage does not exist");

            if (image.DeletedAt != null)
                throw new InvalidOperationException("ProductImage is already soft-deleted");

            image.DeletedAt = DateTime.UtcNow;
            var result = await _repository.UpdateAsync(image);
            if (result == 0)
                throw new InvalidOperationException("Failed to soft-delete ProductImage");

            return true;
        }

        public async Task<bool> Restore(int id)
        {
            var image = await GetByIdAsync(id)
                         ?? throw new InvalidOperationException("ProductImage does not exist");

            if (image.DeletedAt == null)
                throw new InvalidOperationException("ProductImage is already restored");

            image.DeletedAt = null;
            var result = await _repository.UpdateAsync(image);
            if (result == 0)
                throw new InvalidOperationException("Failed to restore ProductImage");

            return true;
        }

    }
}
