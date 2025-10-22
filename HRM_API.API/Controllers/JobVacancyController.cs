using HRM_API.Application.Services;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.General;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobVacancyController(JobVacancyService jobVacancyService) : ControllerBase
    {
        private readonly JobVacancyService _jobVacancyService = jobVacancyService;

        [HttpGet("GetJobVacancies")]
        public async Task<IActionResult> GetJobVacancies([FromQuery] string estado = "", [FromQuery] string id = "")
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _jobVacancyService.GetJobVacanciesAsync(estado, id);

            return Ok(ApiResponses.Ok(response, "OK", "VACANCY_FOUND"));
        }

        [HttpPost("SetJobVacancy")]
        public async Task<IActionResult> SetJobVacancy([FromBody] GeneralFormRequestDto request)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            LoginDBResponseDto user = new()
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = (bool)await _jobVacancyService.SetJobVacancyAsync(request, user);

            return response ? Ok(ApiResponses.Ok("Vacante registrada exitosamente", "VACANCY_CREATED")) : BadRequest(ApiResponses.Fail("VACANCY_NOT_CREATED", "No se ha registrado la vacante"));
        }

        [HttpPut("UpdateJobVacancy")]
        public async Task<IActionResult> UpdateJobVacancy([FromBody] GeneralFormRequestDto request)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            LoginDBResponseDto user = new()
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = (bool)await _jobVacancyService.UpdateJobVacancyAsync(request, user);

            return response ? Ok(ApiResponses.Ok("Vacante actualizada exitosamente", "VACANCY_CREATED")) : BadRequest(ApiResponses.Fail("VACANCY_NOT_CREATED", "No se ha actualizado la vacante"));
        }
    }
}
