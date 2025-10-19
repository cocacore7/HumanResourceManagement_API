using HRM_API.Core.Dtos.General;
using HRM_API.Core.Interfaces.Catalog;

namespace HRM_API.Application.Services
{
    public class CatalogService(ICatalogRepository repository)
    {
        private readonly ICatalogRepository _repository = repository;

        public async Task<CatalogRequestDto?> GetJobCatalogAsync()
        {
            var responsedb = await _repository.GetJobCatalogAsync();

            CatalogRequestDto response = new() { Response = responsedb ?? [] };

            return response;
        }

        public async Task<CatalogRequestDto?> GetTownCatalogAsync()
        {
            var responsedb = await _repository.GetTownCatalogAsync();

            CatalogRequestDto response = new() { Response = responsedb ?? [] };

            return response;
        }

        public async Task<CatalogRequestDto?> GetTermsAndConditionsCatalogAsync()
        {
            var responsedb = await _repository.GetTermsAndConditionsCatalogAsync();

            CatalogRequestDto response = new() { Response = responsedb ?? [] };

            return response;
        }

        public async Task<CatalogRequestDto?> GetVacancyReasonCatalogAsync()
        {
            var responsedb = await _repository.GetVacancyReasonCatalogAsync();

            CatalogRequestDto response = new() { Response = responsedb ?? [] };

            return response;
        }

        public async Task<CatalogRequestDto?> GetVacancyTypeCatalogAsync()
        {
            var responsedb = await _repository.GetVacancyTypeCatalogAsync();

            CatalogRequestDto response = new() { Response = responsedb ?? [] };

            return response;
        }
    }
}
