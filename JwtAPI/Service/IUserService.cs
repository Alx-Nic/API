using JwtAPI.Models;
using JwtAPI.Models.Requests;
using JwtAPI.Models.Responses;

namespace JwtAPI.Service
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
    }

}
