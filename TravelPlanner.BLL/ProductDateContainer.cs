using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.BLL
{
    public class ProductDateContainer
    {
        private readonly DbManager _db;

        public ProductDateContainer(DbManager db)
        {
            _db = db;
        }

        public async Task<int> CreateProductDateAsync(ProductDate productDate)
        {
            if (productDate == null)
            {
                throw new ArgumentNullException(nameof(productDate), "ProductDate cannot be null");
            }

            var result = await _db.InsertWithInt32IdentityAsync(productDate);
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
                throw new ArgumentException("ProductDate ID must be positive", nameof(id));
            }

            return await _db.ProductDates.FirstOrDefaultAsync(pd => pd.ID == id);
        }

        public async Task UpdateProductDateAsync(ProductDate productDate)
        {
            if (productDate == null)
            {
                throw new ArgumentNullException(nameof(productDate), "ProductDate cannot be null");
            }

            if (productDate.ID <= 0)
            {
                throw new ArgumentException("ProductDate must have a valid ID", nameof(productDate));
            }

            var result = await _db.UpdateAsync(productDate);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to update ProductDate");
            }
        }

        public async Task<IEnumerable<ProductDate>> GetAllActiveProductDatesAsync()
        {
            var productDates = await _db.ProductDates
                                  .Where(pd => pd.IsActive)
                                  .ToListAsync();

            if (productDates == null || !productDates.Any())
            {
                throw new InvalidOperationException("No active ProductDates found");
            }

            return productDates;
        }

        public async Task SoftDeleteProductDateAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductDate ID must be positive", nameof(id));
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
            await UpdateProductDateAsync(productDate);
        }
    }
}
