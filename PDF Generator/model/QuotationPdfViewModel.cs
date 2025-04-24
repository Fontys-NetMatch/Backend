using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_Generator.model
{
    public class QuotationPdfViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public List<ProductDateViewModel> ProductDates { get; set; } = new();
    }
}
