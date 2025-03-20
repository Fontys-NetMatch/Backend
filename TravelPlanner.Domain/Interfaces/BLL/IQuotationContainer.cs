using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.Domain.Interfaces.BLL
{
    public interface IQuotationContainer
    {
        void CreateQuotation(Quotation quotation);
        Task<IEnumerable<Quotation>> GetAllActiveQuotationsAsync();
        Task<Quotation?> GetQuotationByIdAsync(int id);
        Task SoftDeleteQuotation(int id);
        Task UpdateQuotation(Quotation quotation);
    }
}
