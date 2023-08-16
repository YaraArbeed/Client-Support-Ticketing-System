using Microsoft.AspNetCore.Mvc;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;
using Ticketing.Services.Interface;

namespace Ticketing.API.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInParam param)
        {
            var token = await _userService.LoginAsync(param);

            if (token == null)
            {
                return BadRequest(); // Invalid credentials
            }

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationParam param)
        {
            
            var message = await _userService.RegisterUserAsync(param);
            return Ok( message );
        }
    }
}
