using HRM_API.Application.Services;
using HRM_API.Core.Dtos.General;
using Microsoft.AspNetCore.Mvc;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController(CatalogService catalogService) : ControllerBase
    {
        private readonly CatalogService _catalogService = catalogService;

        [HttpGet("GetJobCatalog")]
        public async Task<IActionResult> GetJobCatalog()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _catalogService.GetJobCatalogAsync();

            return Ok(response);
        }

        [HttpGet("GetTownCatalog")]
        public async Task<IActionResult> GetTownCatalog()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _catalogService.GetTownCatalogAsync();

            return Ok(response);
        }

        [HttpGet("GetTermsAndConditionsCatalog")]
        public async Task<IActionResult> GetTermsAndConditionsCatalog()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _catalogService.GetTermsAndConditionsCatalogAsync();

            return Ok(response);
        }


        [HttpGet("GetVacancyReasonCatalog")]
        public async Task<IActionResult> GetVacancyReasonCatalog()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _catalogService.GetVacancyReasonCatalogAsync();

            return Ok(response);
        }

        [HttpGet("GetVacancyTypeCatalog")]
        public async Task<IActionResult> GetVacancyTypeCatalog()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(new ErrorDto { Error = "Token inválido" });

            var response = await _catalogService.GetVacancyTypeCatalogAsync();

            return Ok(response);
        }
    }
}
