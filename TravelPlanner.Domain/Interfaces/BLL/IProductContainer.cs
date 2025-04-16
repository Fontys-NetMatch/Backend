using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Request.Product;

namespace TravelPlanner.Domain.Interfaces.BLL
{
    public interface IProductContainer
    {
<<<<<<< Updated upstream
        Task<List<Product>> GetAll();
        Task<List<Product>> GetAllActive();
        Task<Product?> GetById(int id);
        Task Create(ProductData product);
        Task Update(int id, ProductData product);
        Task SoftDelete(int id);
=======
        Task CreateProduct(ProductCreateData product);
        Task<List<Product>> GetAllActiveProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task SoftDeleteProduct(int id);
        Task UpdateProduct(ProductUpdateData product);
>>>>>>> Stashed changes
    }
}
