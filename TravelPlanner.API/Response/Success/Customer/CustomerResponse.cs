using TravelPlanner.API.Response;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.API.Response.Success
{
    public record CustomerResponse : BaseResponse
    {
        public int Id { get; init; }
        public string Firstname { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public string? Phone { get; init; }

        public CustomerResponse(Customer customer, string message) : base(message)
        {
            Id = customer.ID;
            Firstname = customer.Firstname;
            Surname = customer.Surname;
            Email = customer.Email;
            Phone = customer.Phone;
        }
    }
}
