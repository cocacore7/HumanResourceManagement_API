using HRM_API.Application.Services;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.General;
using Microsoft.AspNetCore.Mvc;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(AuthorizationService authService, IConfiguration configuration) : ControllerBase
    {
        private readonly AuthorizationService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var token = await _authService.AuthenticateAsync(request.Name, request.Password, request.Role);

            if (token == null)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Credenciales inválidas"));

            return Ok(ApiResponses.Ok(new LoginResponseDto { Token = token, ExpiredDate = DateTime.Now.AddMinutes(int.Parse(configuration["Jwt:ExpireMinutes"] ?? "60")) }, "OK", "LOGIN_SUCCES"));
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request?.OldToken))
                return BadRequest(ApiResponses.Fail("400", "Token no proporcionado"));

            var newToken = await _authService.RefreshTokenAsync(request.OldToken);

            if (newToken == null)
                // Usa la nueva respuesta semántica TokenExpired()
                return Unauthorized(ApiResponses.TokenExpired());

            return Ok(ApiResponses.Ok(
                new { token = newToken, ExpiredDate = DateTime.Now.AddMinutes(int.Parse(configuration["Jwt:ExpireMinutes"] ?? "60")) }, "OK", "REFRESH_SUCCES"
            ));
        }

        [HttpPost("SendRecoveryCode")]
        public async Task<IActionResult> SendRecoveryCode([FromBody] SendRecoveryCodeRequestDto request)
        {
            var response = await _authService.SendRecoveryCodeAsync(request);

            return response is null ? 
                BadRequest(ApiResponses.Fail("BAD_REQUEST", "Error al enviar codigo de recuperacion")) : 
                Ok(ApiResponses.Ok(new SendRecoveryCodeResponseDto { Response = response ?? "Error al generar codigo de recuperacion" }, "OK", "RECOVERY_SUCCES"));
        }

        [HttpPost("GenerateNewPassword")]
        public async Task<IActionResult> GenerateNewPassword([FromBody] GenerateNewPasswordRequestDto request)
        {
            var response = await _authService.GenerateNewPasswordAsync(request);

            return response is null ?
                BadRequest(ApiResponses.Fail("BAD_REQUEST", "Error al enviar nueva contraseña")) : 
                Ok(ApiResponses.Ok(new GenerateNewPasswordResponseDto { Response = response ?? "Error al generar nueva contraseña" }, "OK", "GENERATE_SUCCES"));
        }
    }
}
