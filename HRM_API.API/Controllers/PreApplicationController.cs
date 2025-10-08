using HRM_API.Application.Services;
using HRM_API.Core.Dtos.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("GetPreApplications")]
        public async Task<IActionResult> GetPreApplications()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("Token inválido");

            var response = await _preApplicationService.GetPreApplicationsAsync();

            return Ok(response);
        }
    }
}
