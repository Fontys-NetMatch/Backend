using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.Domain.Interfaces.BLL
{
    public interface IProductContainer
    {
        void CreateProduct(Product product);
        Task<IEnumerable<Product>> GetAllActiveProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task SoftDeleteProduct(int id);
        Task UpdateProduct(Product product);
    }
}
