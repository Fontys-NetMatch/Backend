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

namespace TravelPlanner.Domain.Interfaces.BLL.Container
{
    public interface IProductTranslationContainer
    {
        Task<List<ProductTranslation>> GetAll(string isoCode);
        Task<List<ProductTranslation>> GetAllActive(string isoCode);
        Task<ProductTranslation?> GetById(int id);
        Task<ProductTranslation?> GetByIdAndIso(int id, string isoCode);
        Task Create(int productId, ProductTranslationData product);
        Task Update(int translationId, ProductTranslationUpdateData product);
        Task<bool> SoftDelete(int translationId);
        Task<bool> Restore(int translationId);
    }
}
