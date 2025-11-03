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
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _catalogService.GetJobCatalogAsync();

            return Ok(ApiResponses.Ok(response, "OK", "JOB_CATALOG_FOUND"));
        }

        [HttpGet("GetActiveJobCatalog")]
        public async Task<IActionResult> GetActiveJobCatalog()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _catalogService.GetActiveJobCatalogAsync();

            return Ok(ApiResponses.Ok(response, "OK", "JOB_CATALOG_FOUND"));
        }

        [HttpGet("GetActiveJobCatalogPublic")]
        public async Task<IActionResult> GetActiveJobCatalogPublic()
        {
            var response = await _catalogService.GetActiveJobCatalogAsync();

            return Ok(ApiResponses.Ok(response, "OK", "JOB_CATALOG_FOUND"));
        }

        [HttpGet("GetJobCatalogPublic")]
        public async Task<IActionResult> GetJobCatalogPublic()
        {
            var response = await _catalogService.GetJobCatalogAsync();

            return Ok(ApiResponses.Ok(response, "OK", "JOB_CATALOG_FOUND"));
        }

        [HttpGet("GetTownCatalog")]
        public async Task<IActionResult> GetTownCatalog()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _catalogService.GetTownCatalogAsync();

            return Ok(ApiResponses.Ok(response, "OK", "TOWN_CATALOG_FOUND"));
        }

        [HttpGet("GetTownCatalogPublic")]
        public async Task<IActionResult> GetTownCatalogPublic()
        {
            var response = await _catalogService.GetTownCatalogAsync();

            return Ok(ApiResponses.Ok(response, "OK", "TOWN_CATALOG_FOUND"));
        }

        [HttpGet("GetTermsAndConditionsCatalog")]
        public async Task<IActionResult> GetTermsAndConditionsCatalog()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _catalogService.GetTermsAndConditionsCatalogAsync();

            return Ok(ApiResponses.Ok(response, "OK", "TERMS_CATALOG_FOUND"));
        }


        [HttpGet("GetVacancyReasonCatalog")]
        public async Task<IActionResult> GetVacancyReasonCatalog()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _catalogService.GetVacancyReasonCatalogAsync();

            return Ok(ApiResponses.Ok(response, "OK", "VACANCYR_CATALOG_FOUND"));
        }

        [HttpGet("GetVacancyTypeCatalog")]
        public async Task<IActionResult> GetVacancyTypeCatalog()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _catalogService.GetVacancyTypeCatalogAsync();

            return Ok(ApiResponses.Ok(response, "OK", "VACANCYT_CATALOG_FOUND"));
        }

        [HttpGet("GetUsersCatalog")]
        public async Task<IActionResult> GetUsersCatalog([FromQuery] string KeyName = "")
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _catalogService.GetUsersCatalogAsync(KeyName);

            return Ok(ApiResponses.Ok(response, "OK", "USERS_CATALOG_FOUND"));
        }

        [HttpGet("GetRoleCatalog")]
        public async Task<IActionResult> GetRoleCatalog()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _catalogService.GetRoleCatalogAsync();

            return Ok(ApiResponses.Ok(response, "OK", "ROLE_CATALOG_FOUND"));
        }

        [HttpGet("GetRoleCataloPublic")]
        public async Task<IActionResult> GetRoleCataloPublic()
        {
            var response = await _catalogService.GetRoleCatalogAsync();

            return Ok(ApiResponses.Ok(response, "OK", "ROLE_CATALOG_FOUND"));
        }
    }
}
