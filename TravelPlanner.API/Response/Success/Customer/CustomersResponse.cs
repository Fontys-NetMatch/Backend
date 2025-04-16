namespace TravelPlanner.API.Response.Success.Customer
{
    public record CustomersResponse : BaseResponse
    {
        public List<Domain.Models.Entities.Customer> Customers { get; set; }

        public CustomersResponse(List<Domain.Models.Entities.Customer> customers)
        {
            Customers = customers;
        }
    }
}
