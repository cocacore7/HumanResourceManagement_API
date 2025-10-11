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

        [HttpGet("GetJobVacancies/{filePath}")]
        public async Task<IActionResult> GetFileBase64(string filePath)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _fileService.GetFileBase64Async(filePath);

            return Ok(response);
        }

        [HttpPost("SetFile")]
        public async Task<IActionResult> SetFile(GeneralFormRequestDto request)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            LoginDBResponseDto user = new()
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = await _fileService.SetFileAsync(request, user);

            return Ok(response);
        }
    }
}
