using HRM_API.Application.Services;
using HRM_API.Core.Dtos.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobVacancyController : ControllerBase
    {
        private readonly JobVacancyService _jobVacancyService;

        public JobVacancyController(JobVacancyService jobVacancyService)
        {
            _jobVacancyService = jobVacancyService;
        }

        [HttpGet("GetJobVacancies/{estado}")]
        public async Task<IActionResult> GetJobVacancies(string estado)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("Token inválido");

            var response = await _jobVacancyService.GetJobVacanciesAsync(estado);

            return Ok(response);
        }
    }
}
