using HRM_API.Application.Services;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Dtos.PreApplication;
using Microsoft.AspNetCore.Mvc;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreApplicationController(PreApplicationService preApplicationService) : ControllerBase
    {
        private readonly PreApplicationService _preApplicationService = preApplicationService;

        [HttpGet("GetPreApplications")]
        public async Task<IActionResult> GetPreApplications([FromQuery] string estado = "", [FromQuery] string id = "")
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _preApplicationService.GetPreApplicationsAsync(estado, id);

            return Ok(response);
        }

        [HttpPost("SetPreApplications")]
        public async Task<IActionResult> SetPreApplications([FromBody] SetPreApplicationsRequestDto request)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _preApplicationService.SetPreApplicationsAsync(request);

            return Ok(response);
        }
    }
}
