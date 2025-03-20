namespace TravelPlanner.API.Response.Success
{
    public record QuotationsResponse : BaseResponse
    {
        public List<QuotationResponse> Quotations { get; set; }

        public QuotationsResponse(List<QuotationResponse> quotations, string message) : base(message)
        {
            Quotations = quotations;
        }
    }
}
