using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Request.Product;

namespace TravelPlanner.Domain.Interfaces.BLL
{
    public interface IProductContainer
    {
        Task<List<Product>> GetAll(ProductFiltersData filters);
        Task<Product?> GetById(int id);
        Task Create(ProductData product);
        Task Update(int id, ProductData product);
        Task SoftDelete(int id);
        Task Restore(int id);
    }
}
