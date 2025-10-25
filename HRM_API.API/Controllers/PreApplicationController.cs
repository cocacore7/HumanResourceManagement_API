using HRM_API.Application.Services;
using HRM_API.Configuration;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.General;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreApplicationController(PreApplicationService preApplicationService, ISettings settings) : ControllerBase
    {
        private readonly PreApplicationService _preApplicationService = preApplicationService;
        private readonly ISettings _settings = settings;

        [HttpGet("GetPreApplications")]
        public async Task<IActionResult> GetPreApplications([FromQuery] string estado = "", [FromQuery] string id = "")
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _preApplicationService.GetPreApplicationsAsync(estado, id);

            return Ok(ApiResponses.Ok(response, "OK", "PREAPPLICATION_FOUND"));
        }

        [HttpPost("SetPreApplications")]
        public async Task<IActionResult> SetPreApplications([FromBody] GeneralFormRequestDto request)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            LoginDBResponseDto user = new()
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = await _preApplicationService.SetPreApplicationsAsync(request, user);

            return Ok(ApiResponses.Ok(response, "OK", "PREAPPLICATION_CREATE"));
        }

        [HttpPost("SetPreApplicationsPublic")]
        public async Task<IActionResult> SetPreApplicationsPublic([FromBody] GeneralFormRequestDto request)
        {
            LoginDBResponseDto user = new()
            {
                IdUser = _settings.PublicUserId.ToString() ?? string.Empty,
                Name = string.Empty,
                RoleId = string.Empty
            };

            var response = await _preApplicationService.SetPreApplicationsAsync(request, user);

            return Ok(ApiResponses.Ok(response, "OK", "PREAPPLICATION_CREATE"));
        }

        [HttpPut("UpdatePreApplications")]
        public async Task<IActionResult> UpdatePreApplications([FromBody] GeneralFormRequestDto request)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            LoginDBResponseDto user = new()
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = await _preApplicationService.UpdatePreApplicationsAsync(request, user);

            return Ok(ApiResponses.Ok(response, "OK", "PREAPPLICATION_UPDATE"));
        }
    }
}
