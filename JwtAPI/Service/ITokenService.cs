using JwtAPI.Models;

namespace JwtAPI.Service
{
    public interface ITokenService
    {
        string generateJWTToken(User user);
    }
}
