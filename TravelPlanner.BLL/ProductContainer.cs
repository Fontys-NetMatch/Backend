using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Interfaces.BLL;
using System.Linq;
using System;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.BLL;

public class ProductContainer : IProductContainer
{
    private readonly DbManager _db;

    public ProductContainer(DbManager db)
    {
        _db = db;
    }

    public void CreateProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }

        if (string.IsNullOrEmpty(product.Location) || product.Taxes <= 0)
        {
            throw new ArgumentException("Invalid product data");
        }

        var result = _db.InsertWithInt32Identity(product);
        if (result <= 0)
        {
            throw new InvalidOperationException("Failed to create product in the database");
        }
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Product ID must be positive", nameof(id));
        }

        return await _db.Products.FirstOrDefaultAsync(p => p.ID == id);
    }

    public async Task UpdateProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }

        if (product.ID <= 0)
        {
            throw new ArgumentException("Product must have a valid ID");
        }

        var existingProduct = await GetProductByIdAsync(product.ID);
        if (existingProduct == null)
        {
            throw new InvalidOperationException("Product does not exist and cannot be updated");
        }

        var result = await _db.UpdateAsync(product);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to update product");
        }
    }

    public async Task SoftDeleteProduct(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Product ID must be positive", nameof(id));
        }

        var product = await GetProductByIdAsync(id);
        if (product == null)
        {
            throw new InvalidOperationException("Product does not exist and cannot be SoftDeleted");
        }

        if (!product.IsActive)
        {
            throw new InvalidOperationException("Product is already inactive");
        }

        product.IsActive = false;
        await UpdateProduct(product);

    }

    public async Task<IEnumerable<Product>> GetAllActiveProductsAsync()
    {
        var products = await _db.Products
                                .Where(p => p.IsActive)
                                .ToListAsync();
        if (products == null || !products.Any())
        {
            throw new InvalidOperationException("No active products found");
        }

        return products;
    }

  
}
