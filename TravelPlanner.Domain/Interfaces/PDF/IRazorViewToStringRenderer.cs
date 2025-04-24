using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.Domain.Interfaces.PDF
{
    public interface IRazorViewToStringRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
