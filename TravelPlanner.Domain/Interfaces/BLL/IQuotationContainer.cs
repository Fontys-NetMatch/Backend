using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Request.Quotation;

namespace TravelPlanner.Domain.Interfaces.BLL
{
    public interface IQuotationContainer
    {
        void CreateQuotation(QuotationData quotation);
        Task<List<Quotation>> GetAllQuotations();
        Task<List<Quotation>> GetAllActiveQuotations();
        Task<Quotation?> GetQuotationById(int id);
        Task SoftDeleteQuotation(int id);
        Task UpdateQuotation(QuotationUpdateData quotation);
    }
}
