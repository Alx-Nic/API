using JwtAPI.Helpers;
using JwtAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAPI.Service
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly double expiryMinutes = 15;

        public TokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public string generateJWTToken(User user)
        {
            var now = DateTime.UtcNow;

            var nowInUnix = new DateTimeOffset(now).ToUnixTimeMilliseconds();
            

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JTI used as a nonce to avoid replay attacks
                new Claim(JwtRegisteredClaimNames.Iat, now.ToString() ,ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Iss, "Test"),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("id", user.Id.ToString()),

            };
            
            var tokenHandler = new JwtSecurityTokenHandler();

            var signingKey = Encoding.UTF8.GetBytes(_appSettings.Value.Secret);

            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: now,
                expires: now.AddDays(15),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256)
                );

            var signedToken = tokenHandler.WriteToken(token);

            return signedToken;


        }
    }
}
