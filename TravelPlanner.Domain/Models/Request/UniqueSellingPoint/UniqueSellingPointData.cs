using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.Domain.Models.Request.UniqueSellingPoint
{
    public class UniqueSellingPointData
    {
        public string Type { get; set; } = null!;
        public string? Value { get; set; }
    }
}
