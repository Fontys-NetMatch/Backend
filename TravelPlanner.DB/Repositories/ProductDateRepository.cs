using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Repositories
{
    public class ProductDateRepository : IProductDateRepository
    {
        private readonly DbManager _db;

        public ProductDateRepository(DbManager db)
        {
            _db = db;
        }

        public Task<int> CreateAsync(ProductDate productDate)
        {
            return _db.InsertWithInt32IdentityAsync(productDate);
        }

        public Task<ProductDate?> GetByIdAsync(int id)
        {
            return _db.ProductDates.FirstOrDefaultAsync(pd => pd.Id == id);
        }

        public Task<int> UpdateAsync(ProductDate productDate)
        {
            return _db.UpdateAsync(productDate);
        }

        public Task<List<ProductDate>> GetAllActiveAsync()
        {
            return _db.ProductDates
                      .Where(pd => pd.IsActive)
                      .ToListAsync();
        }
    }
}
