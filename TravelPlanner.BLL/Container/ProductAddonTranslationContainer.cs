using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.DB.Repositories;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.BLL.Container;

public class ProductAddonTranslationContainer : IProductAddonTranslationContainer
{
    private readonly IProductAddonTranslationRepository _repository;

    public ProductAddonTranslationContainer(IProductAddonTranslationRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> CreateAsync(ProductAddonTranslation translation)
    {
        if (translation == null)
            throw new ArgumentNullException(nameof(translation), "ProductAddonTranslation cannot be null");

        var result = await _repository.CreateAsync(translation);
        if (result <= 0)
            throw new InvalidOperationException("Failed to create ProductAddonTranslation in the database");

        return result;
    }

    public async Task<ProductAddonTranslation?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ProductAddonTranslation Id must be positive", nameof(id));

        return await _repository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(ProductAddonTranslation translation)
    {
        if (translation == null)
            throw new ArgumentNullException(nameof(translation), "ProductAddonTranslation cannot be null");

        if (translation.Id <= 0)
            throw new ArgumentException("ProductAddonTranslation must have a valid Id");

        var result = await _repository.UpdateAsync(translation);
        if (result == 0)
            throw new InvalidOperationException("Failed to update ProductAddonTranslation");
    }

    public async Task<List<ProductAddonTranslation>> GetByLangIsoCodeAsync(string langIsoCode)
    {
        if (string.IsNullOrWhiteSpace(langIsoCode))
            throw new ArgumentException("Language ISO code must be provided", nameof(langIsoCode));

        var translations = await _repository.GetByLangIsoCodeAsync(langIsoCode);
        if (translations is not { Count: > 0 })
            throw new InvalidOperationException("No translations found for the given language ISO code");

        return translations;
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ProductAddonTranslation Id must be positive", nameof(id));

        var translation = await GetByIdAsync(id);
        if (translation == null)
            throw new InvalidOperationException("ProductAddonTranslation does not exist and cannot be deleted");

        if (await _repository.DeleteAsync(translation) == 0)
            throw new InvalidOperationException("Failed to delete ProductAddonTranslation");
    }
}