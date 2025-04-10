using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;
using TravelPlanner.Domain.Models.Request.Product;
using TravelPlanner.Domain.Models.Request.ProductTranslation;

namespace TravelPlanner.Domain.Interfaces.BLL
{
    public interface IProductTranslationContainer
    {
        Task<List<ProductTranslation>> GetAll();
        Task<List<ProductTranslation>> GetAllActive();
        Task<ProductTranslation?> GetById(int id);
        Task Create(int productId, ProductTranslationData product);
        Task Update(int translationId, ProductTranslationUpdateData product);
        Task Delete(int translationId);
    }
}
