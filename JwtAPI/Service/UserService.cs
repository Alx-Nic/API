using JwtAPI.Helpers;
using JwtAPI.Models;
using JwtAPI.Models.Enums;
using JwtAPI.Models.Requests;
using JwtAPI.Models.Responses;
using Microsoft.Extensions.Options;

namespace JwtAPI.Service
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private AppSettings _appSettings;

        private List<User> _users = new List<User>
    {
        new User 
        { 
            Id = new Guid("c12e0fdc-faef-4314-af77-dbeff471f38e"), 
            FirstName = "Test", 
            LastName = "Test",
            UserName = "admin", 
            Password = "admin",
            RecordState = 1,
            Role = "Admin"
        },
        new User
        {
            Id = new Guid("c12e0fdc-faef-4314-af77-dbeff471f382"),
            FirstName = "Test",
            LastName = "Test",
            UserName = "user",
            Password = "user",
            RecordState = 1,
            Role = "User"
        }
    };

        public UserService(IOptions<AppSettings> appSettings, ITokenService tokenService)
        {
            _appSettings = appSettings.Value;
            _tokenService = tokenService;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            if (user == null) return null;

            var token = _tokenService.generateJWTToken(user);

            return new AuthenticateResponse(user = user, token = token);


        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(Guid id)
        {
            return _users.FirstOrDefault(x => x.Id == id && x.RecordState != (int)RecordState.Inactive);
        }
    }
}
