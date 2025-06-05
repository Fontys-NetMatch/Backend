using LinqToDB;
using LinqToDB.Linq;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Repositories;

public class ProductTypeRepository : IProductTypeRepository
{
    private readonly DbManager _db;

    public ProductTypeRepository(DbManager db)
    {
        _db = db;
    }

    public Task<ProductType?> GetByIdAsync(int id)
    {
        return _db.ProductTypes
                  .LoadWith(p => p.Translations)
                  .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<List<ProductType>> GetAllAsync()
    {
        return _db.ProductTypes
                  .LoadWith(p => p.Translations)
                  .ToListAsync();
    }

    public Task<List<ProductType>> GetAllActiveAsync()
    {
        return _db.ProductTypes
                  .Where(p => p.IsActive)
                  .LoadWith(p => p.Translations)
                  .ToListAsync();
    }

    public Task<int> CreateAsync(ProductType productType)
    {
        return _db.InsertWithInt32IdentityAsync(productType);
    }

    public Task<int> UpdateAsync(ProductType productType)
    {
        return _db.UpdateAsync(productType);
    }
}
