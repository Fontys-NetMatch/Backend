using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.BLL.Container;

public class ProductTypeContainer: IProductTypeContainer
{
    private readonly IProductTypeRepository _repository;

    public ProductTypeContainer(IProductTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductTypeEntity?> GetById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid product Id", nameof(id));

        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<ProductTypeEntity>> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return products.Count == 0
            ? throw new InvalidOperationException("No products found")
            : products;
    }

    public async Task<List<ProductTypeEntity>> GetAllActive()
    {
        var products = await _repository.GetAllActiveAsync();
        return products.Count == 0
            ? throw new InvalidOperationException("No active products found")
            : products;
    }
}
