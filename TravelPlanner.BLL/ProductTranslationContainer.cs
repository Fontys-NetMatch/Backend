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
using TravelPlanner.Domain.Models.Request.ProductTranslation;

namespace TravelPlanner.BLL;

public class ProductTranslationContainer : IProductTranslationContainer
{
    private readonly DbManager _db;

    public ProductTranslationContainer(DbManager db)
    {
        _db = db;
    }
    
    public async Task<ProductTranslation?> GetById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product translation ID", nameof(id));
        }

        return await _db.ProductTranslations.FirstOrDefaultAsync(p => p.ID == id);
    }

    public async Task<List<ProductTranslation>> GetAll()
    {
        var translations = await _db.ProductTranslations
            .Where(p => p.IsActive)
            .ToListAsync();
        if (translations == null)
        {
            throw new InvalidOperationException("No product translations found");
        }

        return translations;
    }

    public async Task<List<ProductTranslation>> GetAllActive()
    {
        var translations = await _db.ProductTranslations
            .Where(p => p.IsActive)
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

        var translationId = await _db.InsertWithInt32IdentityAsync(new ProductTranslation()
        {
            Product_ID = productId,
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
            throw new ArgumentException("Invalid product translation ID");
        }
        if (string.IsNullOrEmpty(data.Name) || string.IsNullOrEmpty(data.Description) )
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

        var result = await _db.UpdateAsync(existingTranslation);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to update product translation");
        }
    }

    public async Task Delete(int translationId)
    {
        if (translationId <= 0)
        {
            throw new ArgumentException("Invalid product translation ID");
        }

        var existingTranslation = await GetById(translationId);
        if (existingTranslation == null)
        {
            throw new InvalidOperationException("Product translation does not exist");
        }

        var result = await _db.DeleteAsync(existingTranslation);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to delete product translation");
        }
    }
}
