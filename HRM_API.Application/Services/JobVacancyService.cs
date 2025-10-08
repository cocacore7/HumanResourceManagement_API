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

        public async Task<GetJobVacanciesResponseDto?> GetJobVacanciesAsync(string estado)
        {
            var vacancy = await _repository.GetJobVacanciesAsync(estado);
            GetJobVacanciesResponseDto response = new GetJobVacanciesResponseDto { Response = vacancy ?? new List<GetJobVacanciesDto>() };

            return (response);
        }
    }
}

