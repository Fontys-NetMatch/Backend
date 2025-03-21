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
        Task CreateProduct(ProductData product);
        Task<List<Product>> GetAllActiveProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task SoftDeleteProduct(int id);
        Task UpdateProduct(ProductUpdateData product);
    }
}
