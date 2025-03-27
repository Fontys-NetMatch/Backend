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
    // Private field to interact with the database
    private readonly DbManager _db;

    // Constructor to initialize DbManager
    public ProductContainer(DbManager db)
    {
        _db = db;
    }

    // Method to create a new product
    public void CreateProduct(Product product)
    {
        // Check if the product object is null
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }

        // Check if the product has valid data (location and taxes)
        if (string.IsNullOrEmpty(product.Location) || product.Taxes <= 0)
        {
            throw new ArgumentException("Invalid product data");
        }

        // Insert the new product into the database
        var result = _db.InsertWithInt32Identity(product);
        if (result <= 0)
        {
            throw new InvalidOperationException("Failed to create product in the database");
        }
    }

    // Method to get a product by ID
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        // Check if the ID is valid
        if (id <= 0)
        {
            throw new ArgumentException("Product ID must be positive", nameof(id));
        }

        // Retrieve product by ID
        return await _db.Products.FirstOrDefaultAsync(p => p.ID == id);
    }

    // Method to update an existing product
    public async Task UpdateProduct(Product product)
    {
        // Check if the product object is null
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }

        // Check if the product has a valid ID
        if (product.ID <= 0)
        {
            throw new ArgumentException("Product must have a valid ID");
        }

        // Check if the product exists before updating
        var existingProduct = await GetProductByIdAsync(product.ID);
        if (existingProduct == null)
        {
            throw new InvalidOperationException("Product does not exist and cannot be updated");
        }

        // Update the product in the database
        var result = await _db.UpdateAsync(product);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to update product");
        }
    }

    // Method to soft delete a product (deactivate)
    public async Task SoftDeleteProduct(int id)
    {
        // Check if the ID is valid
        if (id <= 0)
        {
            throw new ArgumentException("Product ID must be positive", nameof(id));
        }

        // Retrieve product by ID
        var product = await GetProductByIdAsync(id);
        if (product == null)
        {
            throw new InvalidOperationException("Product does not exist and cannot be SoftDeleted");
        }

        // Check if the product is already inactive
        if (!product.IsActive)
        {
            throw new InvalidOperationException("Product is already inactive");
        }

        // Set the product as inactive and update it
        product.IsActive = false;
        await UpdateProduct(product);
    }

    // Method to get all active products
    public async Task<IEnumerable<Product>> GetAllActiveProductsAsync()
    {
        // Retrieve all active products from the database
        var products = await _db.Products
                                .Where(p => p.IsActive)
                                .ToListAsync();

        // Throw exception if no active products found
        if (products == null || !products.Any())
        {
            throw new InvalidOperationException("No active products found");
        }

        return products;
    }
}
