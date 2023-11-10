using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QR.Billings.Business.Interfaces.Services;
using QR.Billings.Business.IO.Login;

namespace QR.Billings.Api.Controllers
{
    /// <summary>
    /// API controller for managing user accounts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService"></param>
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticates a user based on the provided credentials.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <response code="200">Authentication successful. Returns user information and token.</response>
        /// <response code="400">Authentication failed. Invalid credentials.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] LoginInput input)
        {
            var (user, token) = await _userService.AuthenticateAsync(input);
            return Ok(new { user, token });
        }
    }
}
