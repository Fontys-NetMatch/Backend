using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.Domain.Interfaces.BLL.Service
{
    public interface IQuotationService
    {
        Task<double> FlatCommision(int id, double money);
        Task<double> PercentileCommision(int id, double percentile);
    }
}
