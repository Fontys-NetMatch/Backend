using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Interfaces.BLL;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;
using TravelPlanner.Domain.Models.Request.Product;
using TravelPlanner.Domain.Models.Request.ProductType;
using TravelPlanner.DB.Repositories;

namespace TravelPlanner.BLL.Container;

public class ProductTypeContainer: IProductTypeContainer
{
    private readonly IProductTypeRepository _repository;

    public ProductTypeContainer(IProductTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductType?> GetById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid product Id", nameof(id));

        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<ProductType>> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return products.Count == 0
            ? throw new InvalidOperationException("No products found")
            : products;
    }

    public async Task<List<ProductType>> GetAllActive()
    {
        var products = await _repository.GetAllActiveAsync();
        return products.Count == 0
            ? throw new InvalidOperationException("No active products found")
            : products;
    }

    public async Task Create(ProductTypeData data)
    {
        var productType = new ProductType
        {
            IsActive = data.IsActive
        };

        var id = await _repository.CreateAsync(productType);
        if (id <= 0)
            throw new InvalidOperationException("Failed to create product");
    }

    public async Task Update(int id, ProductTypeData data)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid product Id");

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new InvalidOperationException("Product does not exist");

        existing.IsActive = data.IsActive;

        if (await _repository.UpdateAsync(existing) == 0)
            throw new InvalidOperationException("Failed to update product");
    }

    public async Task<bool> SoftDelete(int id)
    {
        var product = await _repository.GetByIdAsync(id)
                      ?? throw new InvalidOperationException("Product does not exist");

        product.IsActive = false;

        if (await _repository.UpdateAsync(product) == 0)
            throw new InvalidOperationException("Failed to delete product");

        return true;
    }

    public async Task<bool> Restore(int id)
    {
        var product = await _repository.GetByIdAsync(id)
                      ?? throw new InvalidOperationException("Product does not exist");

        if (product.IsActive)
            throw new InvalidOperationException("Product is already restored");

        product.IsActive = true;

        if (await _repository.UpdateAsync(product) == 0)
            throw new InvalidOperationException("Failed to restore product");

        return true;
    }
}
