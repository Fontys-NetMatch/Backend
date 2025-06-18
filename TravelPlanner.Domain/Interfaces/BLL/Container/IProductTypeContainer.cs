using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Request.Product;
using TravelPlanner.Domain.Models.Request.ProductType;

namespace TravelPlanner.Domain.Interfaces.BLL.Container
{
    public interface IProductTypeContainer
    {

        Task<List<ProductTypeEntity>> GetAll();
        Task<List<ProductTypeEntity>> GetAllActive();
        Task<ProductTypeEntity?> GetById(int id);

    }
}
