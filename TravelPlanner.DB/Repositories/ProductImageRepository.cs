using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly DbManager _db;

        public ProductImageRepository(DbManager db)
        {
            _db = db;
        }

        public Task<int> CreateAsync(ProductImage image)
        {
            return _db.InsertWithInt32IdentityAsync(image);
        }

        public Task<ProductImage?> GetByIdAsync(int id)
        {
            return _db.ProductImages.FirstOrDefaultAsync(pi => pi.Id == id);
        }

        public Task<int> UpdateAsync(ProductImage image)
        {
            return _db.UpdateAsync(image);
        }

        public Task<List<ProductImage>> GetAllActiveAsync()
        {
            return _db.ProductImages
                      .Where(i => i.DeletedAt == null)
                      .ToListAsync();
        }

        public Task<List<ProductImage>> GetByProductIdAsync(int productId)
        {
            return _db.ProductImages
                      .Where(i => i.ProductId == productId && i.DeletedAt == null)
                      .ToListAsync();
        }
    }
}
