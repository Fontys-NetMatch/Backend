using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Models.Entities.Product;

namespace TravelPlanner.DB.Repositories
{
    public class ProductAddonRepository : IProductAddonRepository
    {
        private readonly DbManager _db;

        public ProductAddonRepository(DbManager db)
        {
            _db = db;
        }

        public Task<ProductAddon?> GetByIdAsync(int id)
        {
            return _db.ProductAddons.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<int> CreateAsync(ProductAddon productAddon)
        {
            return _db.InsertWithInt32IdentityAsync(productAddon);
        }

        public Task<int> UpdateAsync(ProductAddon productAddon)
        {
            return _db.UpdateAsync(productAddon);
        }

        public Task<List<ProductAddon>> GetAllActiveAsync()
        {
            return _db.ProductAddons.Where(p => p.IsActive).ToListAsync();
        }
    }
}
