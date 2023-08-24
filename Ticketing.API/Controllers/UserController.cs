using Microsoft.AspNetCore.Mvc;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;
using Ticketing.Services.Interface;

namespace Ticketing.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Access the system by providing user credentials:UserName or Email or MobileNumber ,and Password
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create a new user account by filling the registration form
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationParam param)
        {
            
            var message = await _userService.RegisterUserAsync(param);
            return Ok( message );
        }
    }
}
