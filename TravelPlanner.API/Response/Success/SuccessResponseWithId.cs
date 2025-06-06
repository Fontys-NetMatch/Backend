namespace TravelPlanner.API.Response.Success
{
    public record SuccessResponseWithId : SuccessResponse
    {
        public int NewId { get; set; }

        public SuccessResponseWithId(string message, int newId) : base(message)
        {
            NewId = newId;
        }
    }
}