using AuthService.Application.Contracts.Requests;
using AuthService.Application.Features.Auth.Commands;
using AuthService.Application.Features.Auth.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserCommand _registerUser;
        private readonly LoginUserQuery _loginUser;

        public AuthController(
            RegisterUserCommand registerUser,
            LoginUserQuery loginUser)
        {
            _registerUser = registerUser;
            _loginUser = loginUser;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _registerUser.ExecuteAsync(request);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _loginUser.ExecuteAsync(request);
            return result.IsSuccess ? Ok(result) : Unauthorized(result.Error);
        }
    }
}
