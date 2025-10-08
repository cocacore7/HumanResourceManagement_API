using HRM_API.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreApplicationController : ControllerBase
    {
        private readonly PreApplicationService _preApplicationService;

        public PreApplicationController(PreApplicationService preApplicationService)
        {
            _preApplicationService = preApplicationService;
        }

        [HttpGet("GetPreApplications/{estado}")]
        public async Task<IActionResult> GetPreApplications(string estado)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("Token inválido");

            var response = await _preApplicationService.GetPreApplicationsAsync(estado);

            return Ok(response);
        }
    }
}
