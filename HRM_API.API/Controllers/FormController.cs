using HRM_API.Application.Services;
using HRM_API.Core.Dtos.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly FormService _formService;

        public FormController(FormService formService)
        {
            _formService = formService;
        }

        [HttpGet("GetUserModules")]
        public async Task<IActionResult> GetUserModules()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("Token inválido");

            UserDto user = new UserDto 
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = await _formService.GetUserModulesAsync(user.IdUser);

            return Ok(response);
        }
    }
}
