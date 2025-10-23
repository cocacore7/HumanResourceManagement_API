using HRM_API.Core.Dtos.General;
using HRM_API.Core.Interfaces.Catalog;
using HRM_API.Core.Interfaces.Mail;

namespace HRM_API.Application.Services
{
    public class CatalogService(ICatalogRepository repository, IMailRepository mailRepository)
    {
        private readonly ICatalogRepository _repository = repository;
        private readonly IMailRepository _mailRepository = mailRepository;

        public async Task<CatalogRequestDto?> GetJobCatalogAsync()
        {
            //_mailRepository.GetJobInterviewAsync();

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

        public async Task<CatalogUserResponse?> GetUsersCatalogAsync(string KeyName)
        {
            var responsedb = await _repository.GetUsersCatalogAsync(KeyName);

            CatalogUserResponse response = new() { Response = responsedb ?? [] };

            return response;
        }

        public async Task<CatalogRequestDto?> GetRoleCatalogAsync()
        {
            var responsedb = await _repository.GetRoleCatalogAsync();

            CatalogRequestDto response = new() { Response = responsedb ?? [] };

            return response;
        }
    }
}
