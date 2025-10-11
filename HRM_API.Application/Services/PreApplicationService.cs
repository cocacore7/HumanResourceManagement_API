using HRM_API.Core.Dtos.PreApplication;
using HRM_API.Core.Interfaces.PreApplication;

namespace HRM_API.Application.Services
{
    public class PreApplicationService
    {
        private readonly IPreApplicationRepository _repository;

        public PreApplicationService(IPreApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetPreApplicationsResponseDto?> GetPreApplicationsAsync(string estado)
        {
            var form = await _repository.GetPreApplicationsAsync(estado);
            GetPreApplicationsResponseDto response = new GetPreApplicationsResponseDto { Response = form ?? new List<GetPreApplicationsDBResponseDto>() };

            return (response);
        }
    }
}

