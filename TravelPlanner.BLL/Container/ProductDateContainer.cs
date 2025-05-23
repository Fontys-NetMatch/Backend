using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.DB.Repositories;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.BLL.Container
{
    public class ProductDateContainer : IProductDateContainer
    {
        private readonly IProductDateRepository _repository;

        public ProductDateContainer(IProductDateRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateProductDate(ProductDate productDate)
        {
            if (productDate == null)
                throw new ArgumentNullException(nameof(productDate), "ProductDate cannot be null");

            var result = await _repository.CreateAsync(productDate);
            if (result == 0)
                throw new InvalidOperationException("Failed to create ProductDate in the database");

            return result;
        }

        public async Task<ProductDate?> GetProductDateById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ProductDate Id must be positive", nameof(id));

            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateProductDate(ProductDate productDate)
        {
            if (productDate == null)
                throw new ArgumentNullException(nameof(productDate), "ProductDate cannot be null");

            if (productDate.Id <= 0)
                throw new ArgumentException("ProductDate must have a valid Id");

            var result = await _repository.UpdateAsync(productDate);
            if (result == 0)
                throw new InvalidOperationException("Failed to update ProductDate");
        }

        public async Task<IEnumerable<ProductDate>> GetAllActiveProductDates()
        {
            var list = await _repository.GetAllActiveAsync();
            if (list == null || list.Count == 0)
                throw new InvalidOperationException("No active ProductDates found");

            return list;
        }

        public async Task<bool> SoftDelete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductDate Id must be positive", nameof(id));
            }

            var productDate = await GetProductDateById(id);
            if (productDate == null)
            {
                throw new InvalidOperationException("ProductDate does not exist and cannot be soft-deleted");
            }

            if (!productDate.IsActive)
                throw new InvalidOperationException("ProductDate is already inactive");

            productDate.IsActive = false;
            try
            {
                await UpdateProductDate(productDate);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public async Task<bool> Restore(int id)
        {
            var productDate = await GetProductDateByIdAsync(id)
                              ?? throw new InvalidOperationException("Product does not exist");

            var product = await GetProductDateById(id);
            if (product == null)
            {
                throw new InvalidOperationException("Product does not exist");
            }

            if (product.IsActive)
            {
                throw new InvalidOperationException("Product is already restored");

            productDate.IsActive = true;

            var result = await _repository.UpdateAsync(productDate);
            if (result == 0)
                throw new InvalidOperationException("Failed to restore product");

            return true;
        }
    }
