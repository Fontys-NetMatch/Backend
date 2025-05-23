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
using TravelPlanner.Domain.Models.Request.ProductTranslation;

namespace TravelPlanner.BLL.Container;

public class ProductTranslationContainer(DbManager db) : IProductTranslationContainer
{
    public async Task<ProductTranslation?> GetById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product translation Id", nameof(id));
        }

        return await db.ProductTranslations.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ProductTranslation?> GetByIdAndIso(int id, string isoCode)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product translation Id", nameof(id));
        }
        if (isoCode == null)
        {
            throw new ArgumentException("Ïnvalid IsoCode", nameof(isoCode));
        }
        return await db.ProductTranslations
            .Where(p => p.Id == id && p.LangIsoCode == isoCode)
            .FirstOrDefaultAsync();

    }

    public async Task<List<ProductTranslation>> GetAll(string isoCode)
    {
        var translations = await db.ProductTranslations
            .Where(p => p.LangIsoCode == isoCode)
            .ToListAsync();
        if (translations == null)
        {
            throw new InvalidOperationException("No product translations found");
        }

        return translations;
    }

    public async Task<List<ProductTranslation>> GetAllActive(string isoCode)
    {
        var translations = await db.ProductTranslations
            .Where(p => p.IsActive && p.LangIsoCode == isoCode)
            .ToListAsync();
        if (translations == null)
        {
            throw new InvalidOperationException("No product translations found");
        }

        return translations;
    }

    public async Task Create(int productId, ProductTranslationData data)
    {
        if (string.IsNullOrEmpty(data.Name) || string.IsNullOrEmpty(data.Description))
        {
            throw new ArgumentException("Invalid product translation data");
        }

        var translationId = await db.InsertWithInt32IdentityAsync(new ProductTranslation()
        {
            ProductId = productId,
            LangIsoCode = data.LangIsoCode,
            Name = data.Name,
            Description = data.Description,
            IsActive = data.IsActive
        });
        if (translationId <= 0)
        {
            throw new InvalidOperationException("Failed to create product translation");
        }
    }

    public async Task Update(int translationId, ProductTranslationUpdateData data)
    {
        if (translationId <= 0)
        {
            throw new ArgumentException("Invalid product translation Id");
        }
        if (string.IsNullOrEmpty(data.Name) || string.IsNullOrEmpty(data.Description))
        {
            throw new ArgumentException("Invalid product translation data");
        }

        var existingTranslation = await GetById(translationId);
        if (existingTranslation == null)
        {
            throw new InvalidOperationException("Product translation does not exist");
        }

        existingTranslation.Name = data.Name;
        existingTranslation.Description = data.Description;
        existingTranslation.IsActive = data.IsActive;
        
        if (await db.UpdateAsync(existingTranslation) == 0)
        {
            throw new InvalidOperationException("Failed to update product translation");
        }
    }

    public async Task<bool> SoftDelete(int translationId)
    {
        if (translationId <= 0)
        {
            throw new ArgumentException("Invalid product translation Id");
        }

        var existingTranslation = await GetById(translationId);
        if (existingTranslation == null)
        {
            throw new InvalidOperationException("Product translation does not exist");
        }
        existingTranslation.IsActive = false;
        if (await db.UpdateAsync(existingTranslation) == 0)
        {
            throw new InvalidOperationException("Failed to delete product translation");
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

        var result = await db.UpdateAsync(product);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to restore product");
        }

        return true;
    }
}
