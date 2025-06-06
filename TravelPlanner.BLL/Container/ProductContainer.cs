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
using TravelPlanner.DB.Interfaces;

namespace TravelPlanner.BLL.Container;

public class ProductContainer : IProductContainer
{
    private readonly IProductRepository repository;

    public ProductContainer(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Product?> GetById(int id)
    {

<<<<<<< Updated upstream
        if (id <= 0) throw new ArgumentException("Invalid product Id", nameof(id));
        return await repository.GetByIdAsync(id);

=======
        return await _db.Products
            .LoadWith(p => p.ProductType)
            .LoadWith(p => p.ProductType.Translations)
            .LoadWith(p => p.Translations)
            .LoadWith(p => p.Dates)
            .LoadWith(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);
>>>>>>> Stashed changes
    }

    public async Task<List<Product>> GetAll(ProductFiltersData filters)
    {
        var products = await repository.GetFilteredAsync(filters);
        return products ?? throw new InvalidOperationException("No products found");
    }

    public async Task Create(ProductData data)
    {
        if (data.ProductTypeId <= 0) throw new ArgumentException("Invalid product type Id");

        var product = new Product
        {
            StartLocation = data.StartLocation,
            EndLocation = data.EndLocation,
            DeletedAt = data.DeletedAt,
            ProductTypeId = data.ProductTypeId
        };

        if (await repository.CreateAsync(product) <= 0)
            throw new InvalidOperationException("Failed to create product");
    }

    public async Task Update(int id, ProductData data)
    {
        if (id <= 0 || data.ProductTypeId <= 0)
            throw new ArgumentException("Invalid product or product type Id");

        var existing = await GetById(id);
        if (existing == null)
            throw new InvalidOperationException("Product does not exist");

        existing.StartLocation = data.StartLocation;
        existing.EndLocation = data.EndLocation;
        existing.DeletedAt = data.DeletedAt;
        existing.ProductTypeId = data.ProductTypeId;

        if (await repository.UpdateAsync(existing) == 0)
            throw new InvalidOperationException("Failed to update product");
    }

    public async Task<bool> SoftDelete(int id)
    {
        var product = await GetById(id) ?? throw new InvalidOperationException("Product does not exist");
        if (product.DeletedAt != null)
            throw new InvalidOperationException("Product is already deleted");

        product.DeletedAt = DateTime.Now;
        return await repository.UpdateAsync(product) > 0;
    }

    public async Task<bool> Restore(int id)
    {
        var product = await GetById(id) ?? throw new InvalidOperationException("Product does not exist");
        if (product.DeletedAt == null)
            throw new InvalidOperationException("Product is already restored");

        product.DeletedAt = null;
        return await repository.UpdateAsync(product) > 0;
    }
}