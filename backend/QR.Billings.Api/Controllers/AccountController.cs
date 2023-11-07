using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QR.Billings.Business.Interfaces.Services;
using QR.Billings.Business.IO.User;

namespace QR.Billings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserInput input)
        {
            var (user, token) = await _userService.AuthenticateAsync(input);
            return Ok(new { user, token });
        }
    }
}
