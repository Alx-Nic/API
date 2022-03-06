using JwtAPI.Models.Requests;
using JwtAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService { get; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Helpers.Authorize]
        [Route("Users")]
        public IActionResult GetAll()
        {
            var response = _userService.GetAll();

            if (response == null) return NoContent();

            return Ok(response);
        }

        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            return response == null ? BadRequest() : Ok(response);
        }
        
    }
}
