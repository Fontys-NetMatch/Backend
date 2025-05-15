using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.BLL.Container
{
    public class ProductDateContainer(DbManager db) : IProductDateContainer
    {
        public async Task<int> CreateProductDateAsync(ProductDate productDate)
        {
            if (productDate == null)
            {
                throw new ArgumentNullException(nameof(productDate), "ProductDate cannot be null");
            }

            var result = await db.InsertWithInt32IdentityAsync(productDate);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to create ProductDate in the database");
            }

            return result;
        }

        public async Task<ProductDate?> GetProductDateByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductDate Id must be positive", nameof(id));
            }

            return await db.ProductDates.FirstOrDefaultAsync(pd => pd.Id == id);
        }

        public async Task UpdateProductDateAsync(ProductDate productDate)
        {
            if (productDate == null)
            {
                throw new ArgumentNullException(nameof(productDate), "ProductDate cannot be null");
            }

            if (productDate.Id <= 0)
            {
                throw new ArgumentException("ProductDate must have a valid Id", nameof(productDate));
            }

            var result = await db.UpdateAsync(productDate);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to update ProductDate");
            }
        }

        public async Task<IEnumerable<ProductDate>> GetAllActiveProductDatesAsync()
        {
            var productDates = await db.ProductDates
                                  .Where(pd => pd.IsActive)
                                  .ToListAsync();

            if (productDates is not {Count: < 1})
            {
                throw new InvalidOperationException("No active ProductDates found");
            }

            return productDates;
        }

        public async Task<bool> SoftDeleteProductDateAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductDate Id must be positive", nameof(id));
            }

            var productDate = await GetProductDateByIdAsync(id);
            if (productDate == null)
            {
                throw new InvalidOperationException("ProductDate does not exist and cannot be soft-deleted");
            }

            if (!productDate.IsActive)
            {
                throw new InvalidOperationException("ProductDate is already inactive");
            }

            productDate.IsActive = false;
            try
            {
                await UpdateProductDateAsync(productDate);
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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid product Id", nameof(id));
            }

            var product = await GetProductDateByIdAsync(id);
            if (product == null)
            {
                throw new InvalidOperationException("Product does not exist");
            }

            if (product.IsActive)
            {
                throw new InvalidOperationException("Product is already restored");
            }

            product.IsActive = true;

            var result = await db.UpdateAsync(product);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to restore product");
            }
            return true;
        }
    }
}
