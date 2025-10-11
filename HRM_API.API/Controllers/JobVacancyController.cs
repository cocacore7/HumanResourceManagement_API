using HRM_API.Application.Services;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.General;
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

        [HttpGet("GetJobVacancies")]
        public async Task<IActionResult> GetJobVacancies(string estado = "", string id = "")
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _jobVacancyService.GetJobVacanciesAsync(estado, id);

            return Ok(response);
        }

        [HttpPost("SetJobVacancy")]
        public async Task<IActionResult> SetJobVacancy([FromBody] GeneralFormRequestDto request)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            LoginDBResponseDto user = new LoginDBResponseDto
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = (bool)await _jobVacancyService.SetJobVacancyAsync(request, user);

            return response ? Ok("Vacante Registrada Exitosamente") : BadRequest("No se ha registrado la vacante");
        }
    }
}
