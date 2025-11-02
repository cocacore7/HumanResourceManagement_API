using HRM_API.Application.Services;
using HRM_API.Configuration;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Dtos.PreApplication;
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

        [HttpGet("GetPreApplicationsByUser")]
        public async Task<IActionResult> GetPreApplicationsByUser([FromQuery] string estado = "", [FromQuery] string id = "")
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            LoginDBResponseDto user = new()
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = await _preApplicationService.GetPreApplicationsByUserAsync(estado, id, user);

            return Ok(ApiResponses.Ok(response, "OK", "PREAPPLICATION_FOUND"));
        }

        [HttpGet("GetPreApplicationFiles")]
        public async Task<IActionResult> GetPreApplicationFiles([FromQuery] int? id)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _preApplicationService.GetPreApplicationFilesAsync(id);

            if (response == null) { return BadRequest(ApiResponses.Fail("PREAPPLICATION_FILES_NOT_FOUND", "Token inválido")); }
            return Ok(ApiResponses.Ok(response, "OK", "PREAPPLICATION_FILES_FOUND"));
        }

        [HttpGet("GetCountPreApplicationByState")]
        public async Task<IActionResult> GetCountPreApplicationByState([FromQuery] List<string> states)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _preApplicationService.GetCountPreApplicationByStateAsync(states);

            if (response == null) { return BadRequest(ApiResponses.Fail("GET_COUNT_NOT_FOUND", "No se encontraron contadores por estado")); }
            return Ok(ApiResponses.Ok(response, "OK", "GET_COUNT_FOUND"));
        }

        [HttpGet("GetCountPreApplicationByProccess")]
        public async Task<IActionResult> GetCountPreApplicationByProccess()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _preApplicationService.GetCountPreApplicationByProccessAsync();

            if (response == null) { return BadRequest(ApiResponses.Fail("GET_COUNT_NOT_FOUND", "No se encontraron contadores por estado")); }
            return Ok(ApiResponses.Ok(response, "OK", "GET_COUNT_FOUND"));
        }

        [HttpGet("PreAppValidatePublic")]
        public async Task<IActionResult> PreAppValidatePublic([FromQuery] long? dpi, [FromQuery] int? id)
        {
            var response = await _preApplicationService.PreAppValidatePublicAsync(dpi, id);
            if (response == null) { return BadRequest(ApiResponses.Fail("PREAPP_VALIDATION_NOT_FOUND", "No se pudo validar la pre aplicacion solicitada")); }
            return Ok(ApiResponses.Ok(response, "OK", "PREAPP_VALIDATION_FOUND"));
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

        [HttpPatch("AssignTo")]
        public async Task<IActionResult> AssignTo([FromBody] AssignToRequestDto? request)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            LoginDBResponseDto user = new()
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = await _preApplicationService.AssignToAsync(request?.PreapplicationId, user, request?.IsAssign ?? false);
            if(response == null) { return BadRequest(ApiResponses.Fail("ASSIGNTO_NOT_FOUND", "No se pudo asignar pre aplicación")); }
            return Ok(ApiResponses.Ok(response, "OK", "ASSIGNTO_UPDATE"));
        }
    }
}
