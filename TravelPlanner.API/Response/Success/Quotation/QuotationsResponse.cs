namespace TravelPlanner.API.Response.Success.Quotation
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
