using TravelPlanner.API.Response.Success.ProductType;

namespace TravelPlanner.API.Response.Success.User
{
    public record UsersResponse : BaseResponse
    {
        public List<UserResponse> Users { get; set; }

        public UsersResponse(List<UserResponse> users)
        {
            Users = users;
        }
    }
}
