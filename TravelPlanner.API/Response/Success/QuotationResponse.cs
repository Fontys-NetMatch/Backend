namespace TravelPlanner.API.Response.Success
{
    public record QuotationResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }

        public QuotationResponse(int id, string name, bool isActive, int customerId, string message): base(message)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
            CustomerId = customerId;
        }
    }
}
