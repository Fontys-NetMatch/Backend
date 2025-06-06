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

    public Task<ProductTypeEntity?> GetByIdAsync(int id)
    {
        return _db.ProductTypes
                  .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<List<ProductTypeEntity>> GetAllAsync()
    {
        return _db.ProductTypes
                  .ToListAsync();
    }

    public Task<List<ProductTypeEntity>> GetAllActiveAsync()
    {
        return _db.ProductTypes
                  .ToListAsync();
    }

    public Task<int> CreateAsync(ProductTypeEntity productType)
    {
        return _db.InsertWithInt32IdentityAsync(productType);
    }

    public Task<int> UpdateAsync(ProductTypeEntity productType)
    {
        return _db.UpdateAsync(productType);
    }
}
