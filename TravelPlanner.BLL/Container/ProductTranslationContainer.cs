using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Interfaces.BLL;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities.Translations;
using TravelPlanner.Domain.Models.Request.ProductTranslation;
using TravelPlanner.DB.Interfaces;

namespace TravelPlanner.BLL.Container;

public class ProductTranslationContainer: IProductTranslationContainer
{
    private readonly IProductTranslationRepository _repository;

    public ProductTranslationContainer(IProductTranslationRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductTranslation?> GetById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid product translation Id", nameof(id));

        return await _repository.GetByIdAsync(id);
    }

    public async Task<ProductTranslation?> GetByIdAndIso(int id, string isoCode)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid product translation Id", nameof(id));
        if (string.IsNullOrWhiteSpace(isoCode))
            throw new ArgumentException("Invalid ISO code", nameof(isoCode));

        return await _repository.GetByIdAndIsoAsync(id, isoCode);
    }

    public async Task<List<ProductTranslation>> GetAll(string isoCode)
    {
        var list = await _repository.GetAllAsync(isoCode);
        if (list == null || list.Count == 0)
            throw new InvalidOperationException("No product translations found");

        return list;
    }

    public async Task<List<ProductTranslation>> GetAllActive(string isoCode)
    {
        var list = await _repository.GetAllActiveAsync(isoCode);
        if (list == null || list.Count == 0)
            throw new InvalidOperationException("No active product translations found");

        return list;
    }

    public async Task Create(int productId, ProductTranslationData data)
    {
        if (string.IsNullOrWhiteSpace(data.Name) || string.IsNullOrWhiteSpace(data.Description))
            throw new ArgumentException("Invalid product translation data");

        var translation = new ProductTranslation
        {
            ProductId = productId,
            LangIsoCode = data.LangIsoCode,
            Name = data.Name,
            Description = data.Description,
            IsActive = data.IsActive
        };

        var result = await _repository.CreateAsync(translation);
        if (result <= 0)
            throw new InvalidOperationException("Failed to create product translation");
    }

    public async Task Update(int id, ProductTranslationUpdateData data)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid translation Id");
        if (string.IsNullOrWhiteSpace(data.Name) || string.IsNullOrWhiteSpace(data.Description))
            throw new ArgumentException("Invalid product translation data");

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new InvalidOperationException("Product translation does not exist");

        existing.Name = data.Name;
        existing.Description = data.Description;
        existing.IsActive = data.IsActive;

        var result = await _repository.UpdateAsync(existing);
        if (result == 0)
            throw new InvalidOperationException("Failed to update product translation");
    }

    public async Task<bool> SoftDelete(int translationId)
    {
        var existing = await _repository.GetByIdAsync(translationId);
        if (existing == null)
            throw new InvalidOperationException("Product translation does not exist");
        existing.IsActive = false;
        if (await _repository.UpdateAsync(existing) == 0)
        {
            throw new InvalidOperationException("Failed to delete product translation");
        }
        return true;
    }

    public async Task<bool> Restore(int id)
    {
        var translation = await _repository.GetByIdAsync(id)
                           ?? throw new InvalidOperationException("Product does not exist");

        if (translation.IsActive)
            throw new InvalidOperationException("Product is already restored");

        translation.IsActive = true;

        if (await _repository.UpdateAsync(translation) == 0)
            throw new InvalidOperationException("Failed to restore product translation");

        return true;
    }
}
