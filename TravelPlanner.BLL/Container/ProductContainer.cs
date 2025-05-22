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

namespace TravelPlanner.BLL.Container;

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
            throw new ArgumentException("Invalid product Id", nameof(id));
        }

        return await _db.Products
            .LoadWith(p => p.ProductType)
            .LoadWith(p => p.ProductType.Translations)
            .LoadWith(p => p.Translations)
            .LoadWith(p => p.Dates)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetAll(ProductFiltersData filters)
    {       
        var query = _db.Products.AsQueryable();

        if (filters.IsDeleted != null)
        {
            if (!(bool)filters.IsDeleted)
            {
                query = query.Where(p => p.DeletedAt == null);
            }
            else
            {
                query = query.Where(p => p.DeletedAt != null);
            }
        }

        if(filters.TypeId != null)
        {
            query = query.Where(p => p.ProductTypeId == filters.TypeId);
        }

        if (filters.SearchQuery != null)
        {
            query = query
                .Where(p =>
                    p.EndLocation != null &&
                    (
                        p.StartLocation.Contains(filters.SearchQuery) ||
                        p.EndLocation.Contains(filters.SearchQuery) ||
                        p.Translations.Select(t => t.Name).Any(t => t.Contains(filters.SearchQuery)) ||
                        p.Translations.Select(t => t.Description).Any(t => t != null && t.Contains(filters.SearchQuery))
                    )
                );
        }
        if (filters.StartLocation != null)
        {
            query = query.Where(p => p.StartLocation == filters.StartLocation);
        }
        if (filters.EndLocation != null)
        {
            query = query.Where(p => p.EndLocation != null && p.EndLocation == filters.EndLocation);
        }
        if (filters.StartDateTime != null)
        {
            query = query.Where(p => p.Dates.Any(d => d.StartDate == filters.StartDateTime));
        }
        if (filters.EndDateTime != null)
        {
            query = query.Where(p => p.Dates.Any(d => d.EndDate == filters.EndDateTime));
        }
        if (filters.MinPrice != null)
        {
            query = query.Where(p => p.Dates.Any(d => d.Price >= filters.MinPrice));
        }
        if (filters.MaxPrice != null)
        {
            query = query.Where(p => p.Dates.Any(d => d.Price <= filters.MaxPrice));
        }
        if (filters.MinPeople != null)
        {
            query = query.Where(p => p.Dates.Any(d => d.Slots >= filters.MinPeople));
        }

        var products = await query
            .LoadWith(p => p.ProductType)
            .LoadWith(p => p.ProductType.Translations)
            .LoadWith(p => p.Translations)
            .LoadWith(p => p.Dates)
            .ToListAsync();
        if (products == null)
        {
            throw new InvalidOperationException("No products found");
        }

        return products;
    }

    public async Task Create(ProductData data)
    {
        if (data.ProductTypeId <= 0)
        {
            throw new ArgumentException("Invalid product type Id");
        }

        var productId = await _db.InsertWithInt32IdentityAsync(new Product
        {
            StartLocation = data.StartLocation,
            EndLocation = data.EndLocation,
            DeletedAt = data.DeletedAt,
            ProductTypeId = data.ProductTypeId
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
            throw new ArgumentException("Invalid product Id");
        }
        if (data.ProductTypeId <= 0)
        {
            throw new ArgumentException("Invalid product type Id");
        }

        var existingProduct = await GetById(id);
        if (existingProduct == null)
        {
            throw new InvalidOperationException("Product does not exist");
        }

        existingProduct.StartLocation = data.StartLocation;
        existingProduct.EndLocation = data.EndLocation;
        existingProduct.DeletedAt = data.DeletedAt;
        existingProduct.ProductTypeId = data.ProductTypeId;

        var result = await _db.UpdateAsync(existingProduct);
        if (result == 0)
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

        if (product.DeletedAt != null)
        {
            throw new InvalidOperationException("Product is already deleted");
        }

        product.DeletedAt = DateTime.Now;

        var result = await _db.UpdateAsync(product);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to delete product");
        }
        else
        {
            return true;
        }
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

        if (product.DeletedAt == null)
        {
            throw new InvalidOperationException("Product is already restored");
        }

        product.DeletedAt = null;

        var result = await _db.UpdateAsync(product);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to restore product");
        }

        return true;
    }
}


/*using LinqToDB;
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
using RequestProductData = TravelPlanner.Domain.Models.Request.Product.ProductData; // ✅ Alias
using RequestProductFiltersData = TravelPlanner.Domain.Models.Request.Product.ProductFiltersData;


namespace TravelPlanner.BLL.Container;

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
            throw new ArgumentException("Invalid product Id", nameof(id));
        }

        return await _db.Products
            .LoadWith(p => p.ProductType)
            .LoadWith(p => p.ProductType.Translations)
            .LoadWith(p => p.Translations)
            .LoadWith(p => p.Dates)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetAll(ProductFiltersData filters)
    {
        var query = _db.Products.AsQueryable();

        if (filters.IsDeleted != null)
        {
            query = filters.IsDeleted.Value
                ? query.Where(p => p.DeletedAt != null)
                : query.Where(p => p.DeletedAt == null);
        }

        if (filters.TypeId != null)
        {
            query = query.Where(p => p.ProductTypeId == filters.TypeId);
        }

        if (filters.SearchQuery != null)
        {
            query = query.Where(p =>
                p.EndLocation != null &&
                (
                    p.StartLocation.Contains(filters.SearchQuery) ||
                    p.EndLocation.Contains(filters.SearchQuery) ||
                    p.Translations.Any(t => t.Name.Contains(filters.SearchQuery)) ||
                    p.Translations.Any(t => t.Description != null && t.Description.Contains(filters.SearchQuery))
                )
            );
        }

        if (filters.StartLocation != null)
        {
            query = query.Where(p => p.StartLocation == filters.StartLocation);
        }

        if (filters.EndLocation != null)
        {
            query = query.Where(p => p.EndLocation != null && p.EndLocation == filters.EndLocation);
        }

        if (filters.StartDateTime != null)
        {
            query = query.Where(p => p.Dates.Any(d => d.StartDate == filters.StartDateTime));
        }

        if (filters.EndDateTime != null)
        {
            query = query.Where(p => p.Dates.Any(d => d.EndDate == filters.EndDateTime));
        }

        if (filters.MinPrice != null)
        {
            query = query.Where(p => p.Dates.Any(d => d.Price >= filters.MinPrice));
        }

        if (filters.MaxPrice != null)
        {
            query = query.Where(p => p.Dates.Any(d => d.Price <= filters.MaxPrice));
        }

        if (filters.MinPeople != null)
        {
            query = query.Where(p => p.Dates.Any(d => d.Slots >= filters.MinPeople));
        }

        var products = await query
            .LoadWith(p => p.ProductType)
            .LoadWith(p => p.ProductType.Translations)
            .LoadWith(p => p.Translations)
            .LoadWith(p => p.Dates)
            .ToListAsync();

        if (products == null || products.Count == 0)
        {
            throw new InvalidOperationException("No products found");
        }

        return products;
    }

    public async Task Create(RequestProductData data)
    {
        if (data.ProductTypeId <= 0)
        {
            throw new ArgumentException("Invalid product type Id");
        }

        var productId = await _db.InsertWithInt32IdentityAsync(new Product
        {
            StartLocation = data.StartLocation,
            EndLocation = data.EndLocation,
            DeletedAt = data.DeletedAt,
            ProductTypeId = data.ProductTypeId
        });

        if (productId <= 0)
        {
            throw new InvalidOperationException("Failed to create product");
        }
    }

    public async Task Update(int id, RequestProductData data)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product Id");
        }
        if (data.ProductTypeId <= 0)
        {
            throw new ArgumentException("Invalid product type Id");
        }

        var existingProduct = await GetById(id);
        if (existingProduct == null)
        {
            throw new InvalidOperationException("Product does not exist");
        }

        existingProduct.StartLocation = data.StartLocation;
        existingProduct.EndLocation = data.EndLocation;
        existingProduct.DeletedAt = data.DeletedAt;
        existingProduct.ProductTypeId = data.ProductTypeId;

        var result = await _db.UpdateAsync(existingProduct);
        if (result == 0)
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

        if (product.DeletedAt != null)
        {
            throw new InvalidOperationException("Product is already deleted");
        }

        product.DeletedAt = DateTime.Now;

        var result = await _db.UpdateAsync(product);
        if (result == 0)
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

        if (product.DeletedAt == null)
        {
            throw new InvalidOperationException("Product is already restored");
        }

        product.DeletedAt = null;

        var result = await _db.UpdateAsync(product);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to restore product");
        }

        return true;
    }
}*/
