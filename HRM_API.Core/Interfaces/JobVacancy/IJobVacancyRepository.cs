using HRM_API.Core.Dtos.JobVacancy;

namespace HRM_API.Core.Interfaces.JobVacancy
{
    public interface IJobVacancyRepository
    {
        Task<List<GetJobVacanciesDto>?> GetJobVacanciesAsync(string estado, string id);
    }
}
