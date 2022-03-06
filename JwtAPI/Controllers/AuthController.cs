using JwtAPI.Models.Requests;
using JwtAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService { get; }

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<ActionResult<string>> Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            return response == null ? BadRequest() : Ok(response.Token);
        }
        
    }
}
