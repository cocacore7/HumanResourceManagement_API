using HRM_API.Application.Services;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.General;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController(FormService formService) : ControllerBase
    {
        private readonly FormService _formService = formService;

        [HttpGet("GetFormAnswers")]
        public async Task<IActionResult> GetFormAnswers([FromQuery] int PreApplicationId, [FromQuery] int FormId)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _formService.GetFormAnswersAsync(PreApplicationId, FormId);

            return Ok(response);
        }

        [HttpPost("SetFormAnswers")]
        public async Task<IActionResult> SetFormAnswers([FromBody] GeneralFormRequestDto request)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            LoginDBResponseDto user = new()
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = await _formService.SetFormAnswersAsync(request, user);

            return Ok(response);
        }

        [HttpPut("UpdateFormAnswers")]
        public async Task<IActionResult> UpdateFormAnswers([FromBody] GeneralFormRequestDto request)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            LoginDBResponseDto user = new()
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = await _formService.UpdateFormAnswersAsync(request, user);

            return Ok(response);
        }
    }
}
