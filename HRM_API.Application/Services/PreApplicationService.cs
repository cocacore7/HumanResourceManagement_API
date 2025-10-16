using HRM_API.Core.Dtos.PreApplication;
using HRM_API.Core.Interfaces.PreApplication;

namespace HRM_API.Application.Services
{
    public class PreApplicationService(IPreApplicationRepository repository)
    {
        private readonly IPreApplicationRepository _repository = repository;

        public async Task<GetPreApplicationsResponseDto?> GetPreApplicationsAsync(string estado, string id)
        {
            var form = await _repository.GetPreApplicationsAsync(estado, id);
            GetPreApplicationsResponseDto response = new() { Response = form ?? [] };

            return (response);
        }

        public async Task<SetPreApplicationsReponseDto?> SetPreApplicationsAsync(SetPreApplicationsRequestDto request)
        {
            var form = (bool)await _repository.SetPreApplicationsAsync(new());
            SetPreApplicationsReponseDto response = new() { Response = form ? "Pre Aplicacion Registrada Exitosamente" : "Error Al Registrar Pre Aplicacion" };

            return (response);
        }
    }
}

