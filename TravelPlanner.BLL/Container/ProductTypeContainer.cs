using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Interfaces.BLL;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;
using TravelPlanner.Domain.Models.Request.Product;
using TravelPlanner.Domain.Models.Request.ProductType;

namespace TravelPlanner.BLL.Container;

public class ProductTypeContainer(DbManager db) : IProductTypeContainer
{
    public async Task<ProductType?> GetById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product Id", nameof(id));
        }

        return await db.ProductTypes
            .LoadWith(p => p.Translations)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<ProductType>> GetAll()
    {
        var products = await db.ProductTypes
            .LoadWith(p => p.Translations)
            .ToListAsync();
        if (products == null)
        {
            throw new InvalidOperationException("No products found");
        }

        return products;
    }

    public async Task<List<ProductType>> GetAllActive()
    {
        var products = await db.ProductTypes
            .Where(p => p.IsActive)
            .LoadWith(p => p.Translations)
            .ToListAsync();
        if (products == null)
        {
            throw new InvalidOperationException("No products found");
        }

        return products;
    }

    public async Task Create(ProductTypeData data)
    {
        var productId = await db.InsertWithInt32IdentityAsync(new ProductType
        {
            IsActive = data.IsActive
        });
        if (productId <= 0)
        {
            throw new InvalidOperationException("Failed to create product");
        }
    }

    public async Task Update(int id, ProductTypeData data)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product Id");
        }

        var existingProduct = await GetById(id);
        if (existingProduct == null)
        {
            throw new InvalidOperationException("Product does not exist");
        }

        existingProduct.IsActive = data.IsActive;
        
        if (await db.UpdateAsync(existingProduct) == 0)
        {
            throw new InvalidOperationException("Failed to update product");
        }
    }

    public async Task<bool> SoftDelete(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product Id", nameof(id));
        }

        var product = await GetById(id);
        if (product == null)
        {
            throw new InvalidOperationException("Product does not exist");
        }

        product.IsActive = false;
        
        if (await db.UpdateAsync(product) == 0)
        {
            throw new InvalidOperationException("Failed to delete product");
        }
        return true;
    }

    public async Task<bool> Restore(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product Id", nameof(id));
        }

        var product = await GetById(id);
        if (product == null)
        {
            throw new InvalidOperationException("Product does not exist");
        }

        if (product.IsActive)
        {
            throw new InvalidOperationException("Product is already restored");
        }

        product.IsActive = true;
        
        if (await db.UpdateAsync(product) == 0)
        {
            throw new InvalidOperationException("Failed to restore product");
        }
        return true;
    }
}
