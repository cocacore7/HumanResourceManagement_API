using HRM_API.Application.Services;
using HRM_API.Core.Dtos.General;
using Microsoft.AspNetCore.Mvc;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly FileService _fileService;

        public FileController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("GetJobVacancies/{filePath}")]
        public async Task<IActionResult> GetFileBase64(string filePath)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _fileService.GetFileBase64Async(filePath);

            return Ok(response);
        }
    }
}
