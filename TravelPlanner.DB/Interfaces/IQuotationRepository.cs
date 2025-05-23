using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Interfaces
{
    public interface IQuotationRepository
    {
        Task<int> CreateAsync(Quotation quotation);
        Task<List<Quotation>> GetAllActiveAsync();
        Task<List<Quotation>> GetAllAsync();
        Task<Quotation?> GetByIdAsync(int id);
        Task<List<ProductDate>> GetQuotationProductsAsync(int quotationId);
        Task<int> UpdateAsync(Quotation quotation);
    }
}