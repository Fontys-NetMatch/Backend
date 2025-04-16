using TravelPlanner.API.Response;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.API.Response.Success
{
    public record CustomersResponse : BaseResponse
    {
        public List<Customer> Customers { get; set; }

        public CustomersResponse(List<Customer> customers, string message) : base(message)
        {
            Customers = customers;
        }
    }
}
