using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Interfaces.BLL.Service;
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
            double adjustedAmount = await Calculate(id) + money;
            return await RoundUpToTwoDecimalsAsync(adjustedAmount);
        }

        public async Task<double> PercentileCommision(int id, double percentile)
        {
            double adjustedAmount = await Calculate(id) * ((percentile / 100) + 1);
            return await RoundUpToTwoDecimalsAsync(adjustedAmount);
        }

        private async Task<double> Calculate(int id)
        {
            List<ProductDate> products = await _service.GetQuotationProducts(id);
            return products.Sum(p => p.Price);
        }
        
        private static async Task<double> RoundUpToTwoDecimalsAsync(double amount)
        {
            return await Task.Run(() =>
            {
                return Math.Ceiling(amount * 100) / 100;
            });
        }

    }
}
