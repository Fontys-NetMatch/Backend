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

namespace TravelPlanner.BLL;

public class ProductContainer : IProductContainer
{
    private readonly DbManager _db;

    public ProductContainer(DbManager db)
    {
        _db = db;
    }

    public async Task<Product?> GetById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product ID", nameof(id));
        }

        return await _db.Products
            .LoadWith(p => p.Translations)
            .FirstOrDefaultAsync(p => p.ID == id);
    }

    public async Task<List<Product>> GetAllActive()
    {
        var products = await _db.Products
            .Where(p => p.IsActive)
            .LoadWith(p => p.Translations)
            .ToListAsync();
        if (products == null)
        {
            throw new InvalidOperationException("No products found");
        }

        return products;
    }

    public async Task Create(ProductData data)
    {
        if (string.IsNullOrEmpty(data.Location) || data.Taxes <= 0|| data.ProductType_ID <= 0)
        {
            throw new ArgumentException("Invalid product data");
        }

        var productId = await _db.InsertWithInt32IdentityAsync(new Product
        {
            Location = data.Location,
            Taxes = data.Taxes,
            IsActive = data.IsActive,
            ProductType_ID = data.ProductType_ID
        });
        if (productId <= 0)
        {
            throw new InvalidOperationException("Failed to create product");
        }
    }

    public async Task Update(int id, ProductData data)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product ID");
        }
        if (string.IsNullOrEmpty(data.Location) || data.Taxes <= 0|| data.ProductType_ID <= 0)
        {
            throw new ArgumentException("Invalid product data");
        }

        var existingProduct = await GetById(id);
        if (existingProduct == null)
        {
            throw new InvalidOperationException("Product does not exist");
        }

        existingProduct.Location = data.Location;
        existingProduct.Taxes = data.Taxes;
        existingProduct.IsActive = data.IsActive;
        existingProduct.ProductType_ID = data.ProductType_ID;

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
