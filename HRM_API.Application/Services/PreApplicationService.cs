using HRM_API.Core.Dtos.PreApplication;
using HRM_API.Core.Interfaces.PreApplication;

namespace HRM_API.Application.Services
{
    public class PreApplicationService(IPreApplicationRepository repository)
    {
        private readonly IPreApplicationRepository _repository = repository;

        public async Task<GetPreApplicationsResponseDto?> GetPreApplicationsAsync(string estado)
        {
            var form = await _repository.GetPreApplicationsAsync(estado);
            GetPreApplicationsResponseDto response = new() { Response = form ?? [] };

            return (response);
        }
    }
}

