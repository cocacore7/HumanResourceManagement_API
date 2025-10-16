using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.General;
using Microsoft.AspNetCore.Mvc;
using HRM_API.Application.Services;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(AuthorizationService authService) : ControllerBase
    {
        private readonly AuthorizationService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var token = await _authService.AuthenticateAsync(request.Name, request.Password, request.Role);

            if (token == null)
                return Unauthorized(new ErrorDto { Error = "Credenciales inválidas" });

            return Ok(new LoginResponseDto { Token = token });
        }
    }
}
