using SalesSystem.Models.Request;
using SalesSystem.Models.Response;

namespace SalesSystem.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthReq req);
    }
}
