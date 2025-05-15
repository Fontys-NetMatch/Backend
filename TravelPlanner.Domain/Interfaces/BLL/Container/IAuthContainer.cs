using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Request.Auth;

namespace TravelPlanner.Domain.Interfaces.BLL.Container
{
    public interface IAuthContainer
    {

        public User LoginUser(LoginData data);
        public void RegisterUser(RegisterData data);

    }
}