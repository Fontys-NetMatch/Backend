using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Interfaces.PDF;

namespace TravelPlanner.BLL.Service
{
    public class QuotationPDF : IQuotationPDF
    {
        public readonly IPDFService pdf;
        public QuotationPDF(IPDFService pdf)
        {
            this.pdf = pdf;
        }



    }
}
