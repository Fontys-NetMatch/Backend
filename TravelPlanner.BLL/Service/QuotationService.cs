using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.BLL.Service
{
    public class QuotationService : IQuotationService
    {
        private readonly IQuotationContainer _service;

        public QuotationService(IQuotationContainer _service)
        {
            this._service = _service;
        }

        public async Task<double> FlatCommision(int id, double money)
        {
            return await Calculate(id) + money;
        }

        public async Task<double> PercentileCommision(int id, double percentile)
        {
            return await Calculate(id) * ((percentile / 100) + 1);
        }

        private async Task<double> Calculate(int id)
        {
            List<ProductDate> products = await _service.GetQuotationProducts(id);
            return products.Sum(p => p.Price);
        }

    }
}
