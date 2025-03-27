using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Interfaces.BLL;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;
using TravelPlanner.Domain.Models.Request.Product;
using TravelPlanner.Domain.Models.Request.ProductType;

namespace TravelPlanner.BLL;

public class ProductTypeContainer : IProductTypeContainer
{
    private readonly DbManager _db;

    public ProductTypeContainer(DbManager db)
    {
        _db = db;
    }

    public async Task<ProductType?> GetById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product ID", nameof(id));
        }

        return await _db.ProductTypes
            .LoadWith(p => p.Translations)
            .FirstOrDefaultAsync(p => p.ID == id);
    }

    public async Task<List<ProductType>> GetAllActive()
    {
        var products = await _db.ProductTypes
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
        var productId = await _db.InsertWithInt32IdentityAsync(new ProductType
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
            throw new ArgumentException("Invalid product ID");
        }

        var existingProduct = await GetById(id);
        if (existingProduct == null)
        {
            throw new InvalidOperationException("Product does not exist");
        }

        existingProduct.IsActive = data.IsActive;

        var result = await _db.UpdateAsync(existingProduct);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to update product");
        }
    }

    public async Task SoftDelete(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product ID", nameof(id));
        }

        var product = await GetById(id);
        if (product == null)
        {
            throw new InvalidOperationException("Product does not exist");
        }

        if (product.DeletedAt != null)
        {
            throw new InvalidOperationException("Product is already deleted");
        }

        product.IsActive = false;
        product.DeletedAt = DateTime.Now;

        var result = await _db.UpdateAsync(product);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to delete product");
        }
    }

}
