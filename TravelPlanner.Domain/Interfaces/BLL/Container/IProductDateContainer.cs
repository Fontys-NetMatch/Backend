using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.Domain.Interfaces.BLL.Container;

public interface IProductDateContainer
{
    public Task<int> CreateProductDate(ProductDate productDate);
    public Task<ProductDate?> GetProductDateById(int id);
    public Task UpdateProductDate(ProductDate productDate);
    public Task<IEnumerable<ProductDate>> GetAllActiveProductDates();
    public Task<bool> SoftDelete(int id);
    public Task<bool> Restore(int id);
}