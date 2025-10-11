using HRM_API.Application.Services;
using HRM_API.Core.Dtos.General;
using Microsoft.AspNetCore.Mvc;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreApplicationController(PreApplicationService preApplicationService) : ControllerBase
    {
        private readonly PreApplicationService _preApplicationService = preApplicationService;

        [HttpGet("GetPreApplications/{estado}")]
        public async Task<IActionResult> GetPreApplications(string estado)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _preApplicationService.GetPreApplicationsAsync(estado);

            return Ok(response);
        }
    }
}
