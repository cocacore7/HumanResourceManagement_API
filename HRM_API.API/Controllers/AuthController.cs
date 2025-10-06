using HRM_API.Core.Dtos.Authorization;
using HRM_API.Application;
using Microsoft.AspNetCore.Mvc;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthorizationService _authService;

        public AuthController(AuthorizationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.AuthenticateAsync(request.Name, request.Password, request.Role);

            if (token == null)
                return Unauthorized(new { error = "Credenciales inválidas" });

            return Ok(new { Token = token });
        }
    }
}
