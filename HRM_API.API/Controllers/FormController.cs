using HRM_API.Application.Services;
using HRM_API.Core.Dtos.General;
using Microsoft.AspNetCore.Mvc;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController(FormService formService) : ControllerBase
    {
        private readonly FormService _formService = formService;

        [HttpGet("GetFormAnswers")]
        public async Task<IActionResult> GetFormAnswers([FromQuery] int PreApplicationId, [FromQuery] int FormId)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _formService.GetFormAnswersAsync(PreApplicationId, FormId);

            return Ok(response);
        }
    }
}
