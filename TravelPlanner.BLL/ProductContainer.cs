using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Interfaces.BLL;
using System.Linq;
using System;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Request.Product;

namespace TravelPlanner.BLL;

public class ProductContainer : IProductContainer
{
    private readonly DbManager _db;

    public ProductContainer(DbManager db)
    {
        _db = db;
    }

    public async Task CreateProduct(ProductData data)
    {
        if (string.IsNullOrEmpty(data.Location) || data.Taxes <= 0|| data.ProductType_ID <= 0)
        {
            throw new ArgumentException("Invalid product data");
        }

        await _db.BeginTransactionAsync();

        var result = await _db.InsertAsync(new Product
        {
            Location = data.Location,
            Taxes = data.Taxes,
            IsActive = true,
            ProductType_ID = data.ProductType_ID
        });
        if (result <= 0)
        {
            await _db.RollbackTransactionAsync();
            throw new InvalidOperationException("Failed to create product");
        }

        await _db.CommitTransactionAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product ID", nameof(id));
        }

        return await _db.Products.FirstOrDefaultAsync(p => p.ID == id);
    }

    public async Task UpdateProduct(ProductUpdateData data)
    {
        if (data.ID <= 0)
        {
            throw new ArgumentException("Invalid product ID");
        }

        if (string.IsNullOrEmpty(data.Location) || data.Taxes <= 0|| data.ProductType_ID <= 0)
        {
            throw new ArgumentException("Invalid product data");
        }

        var existingProduct = await GetProductByIdAsync(data.ID);
        if (existingProduct == null)
        {
            throw new InvalidOperationException("Product does not exist");
        }

        existingProduct.Location = data.Location;
        existingProduct.Taxes = data.Taxes;
        existingProduct.ProductType_ID = data.ProductType_ID;

        var result = await _db.UpdateAsync(existingProduct);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to update product");
        }
    }

    public async Task SoftDeleteProduct(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product ID", nameof(id));
        }

        var product = await GetProductByIdAsync(id);
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
            throw new InvalidOperationException("Failed to update product");
        }
    }

    public async Task<List<Product>> GetAllActiveProductsAsync()
    {
        var products = await _db.Products
                                .Where(p => p.IsActive)
                                .ToListAsync();
        if (products == null)
        {
            throw new InvalidOperationException("No products found");
        }

        return products;
    }

}
