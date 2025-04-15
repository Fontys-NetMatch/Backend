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
        Task<List<Product>> GetAll();
        Task<List<Product>> GetAllActive();
        Task<List<Product>> GetAllInactive();
        Task<Product?> GetById(int id);
        Task Create(ProductData product);
        Task Update(int id, ProductData product);
        Task SoftDelete(int id);
        Task Restore(int id);
    }
}
