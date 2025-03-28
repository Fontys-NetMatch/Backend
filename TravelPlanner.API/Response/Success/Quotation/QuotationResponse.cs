using TravelPlanner.Domain.Enums;

namespace TravelPlanner.API.Response.Success.Quotation
{
    public record QuotationResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public QuotationStatus Status { get; set; }
        public int CustomerId { get; set; }

        public QuotationResponse(int id, string name, QuotationStatus status, int customerId, string message): base(message)
        {
            Id = id;
            Name = name;
            Status = status;
            CustomerId = customerId;
        }
    }
}
