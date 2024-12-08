using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChocolateFactoryApi.services;

namespace ChocolateFactoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            

            var user = await _authService.RegisterAsync(
                registerDto.Name,
                registerDto.Email,
                registerDto.Username,
                registerDto.Password,
                registerDto.Role);

            return Ok(new { message = "Registration Successful", user });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _authService.LoginAsync(loginDto.Username, loginDto.Password, loginDto.Role);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }
            // Generate JWT token
            var token = await _authService.GenerateJwtToken(user);
            var userId = user.UserId;

            return Ok(new { token,userId });
        }
    
    }
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class RegisterDto
    {    public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

}

