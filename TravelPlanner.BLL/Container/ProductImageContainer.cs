using LinqToDB;
using LinqToDB.Common;
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

        public async Task<int> Create(ProductImage image)
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


        public async Task Update(ProductImage image)
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
        
            var image = await _repository.GetByIdAsync(id);
            if (image == null)
            {
                throw new InvalidOperationException("ProductImage does not exist and cannot be soft-deleted");
            }

            if (image.DeletedAt != null)
                throw new InvalidOperationException("ProductImage is already soft-deleted");

            image.DeletedAt = DateTime.UtcNow;
            try
            {
                await _repository.UpdateAsync(image);
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
            var image = await _repository.GetByIdAsync(id)
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

        public async Task<List<ProductImage>> GetImagesByProductIdAsync(int productId)
        {
            var imagelist = await  _repository.GetByProductIdAsync(productId);
            if (imagelist.IsNullOrEmpty())
            {
                throw new InvalidOperationException("No images found for the Product");
            }
            return imagelist;
        }
    }
}
