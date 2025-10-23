using HRM_API.Application.Services;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.General;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController(FileService fileService) : ControllerBase
    {
        private readonly FileService _fileService = fileService;

        [HttpGet("GetFileBase64")]
        public async Task<IActionResult> GetFileBase64([FromQuery] string filePath = "")
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _fileService.GetFileBase64Async(filePath);

            return Ok(ApiResponses.Ok(response, "OK", "FILE_FOUND"));
        }

        [HttpPost("SetFile")]
        public async Task<IActionResult> SetFile([FromBody] GeneralFormRequestDto request)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            LoginDBResponseDto user = new()
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = await _fileService.SetFileAsync(request, user);

            return Ok(ApiResponses.Ok(response, "OK", "FILE_CREATE"));
        }
    }
}
