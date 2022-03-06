using JwtAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        [Helpers.Authorize]
        [Route("/Users")]
        public IActionResult GetAll()
        {
            var response = _userService.GetAll();

            return response == null ? NoContent() : Ok(response);

        }
    }
}
