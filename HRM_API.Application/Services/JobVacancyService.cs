using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Dtos.JobVacancy;
using HRM_API.Core.Interfaces.JobVacancy;

namespace HRM_API.Application.Services
{
    public class JobVacancyService
    {
        private readonly IJobVacancyRepository _repository;

        public JobVacancyService(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetJobVacanciesResponseDto?> GetJobVacanciesAsync(string estado, string id)
        {
            var vacancy = await _repository.GetJobVacanciesAsync(estado, id);
            GetJobVacanciesResponseDto response = new GetJobVacanciesResponseDto { Response = vacancy ?? new List<GetJobVacanciesDBRequestDto>() };

            return (response);
        }

        public async Task<bool?> CreateJobVacancyAsync(GeneralFormRequestDto payload, LoginDBResponseDto user)
        {
            var newvacancy = (bool)await _repository.CreateJobVacancyAsync(new CreateJobVacancyDBRequestDto());
            bool response = newvacancy;

            return (response);
        }
    }
}

