using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Request.Product;
using EntityProductData = TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.Domain.Interfaces.BLL.Container
{
    public interface IProductContainer
    {
        Task<List<Product>> GetAll(ProductFiltersData filters);
        Task<Product?> GetById(int id);
        Task Create(ProductData product);
        Task Update(int id, ProductData product);
        Task<bool> SoftDelete(int id);
        Task<bool> Restore(int id);
    }
}


/*using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Request.Product;
using EntityProductData = TravelPlanner.Domain.Models.Entities.Products.ProductData;
using RequestProductData = TravelPlanner.Domain.Models.Request.Product.ProductData;

namespace TravelPlanner.Domain.Interfaces.BLL.Container
{
    public interface IProductContainer
    {
        Task<List<Product>> GetAll(ProductFiltersData filters);
        Task<Product?> GetById(int id);
        Task Create(RequestProductData product);
        Task Update(int id, RequestProductData product);
        Task<bool> SoftDelete(int id);
        Task<bool> Restore(int id);
    }
}*/
