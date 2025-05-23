using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.Domain.Interfaces.BLL.Container;

public interface IProductDateContainer
{
    public Task<int> CreateProductDateAsync(ProductDate productDate);
    public Task<ProductDate?> GetProductDateByIdAsync(int id);
    public Task UpdateProductDateAsync(ProductDate productDate);
    public Task<IEnumerable<ProductDate>> GetAllActiveProductDatesAsync();
    public Task<bool> SoftDeleteProductDateAsync(int id);
    public Task<bool> Restore(int id);
}