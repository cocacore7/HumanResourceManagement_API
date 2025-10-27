using HRM_API.Core.Dtos.JobVacancy;

namespace HRM_API.Core.Interfaces.JobVacancy
{
    public interface IJobVacancyRepository
    {
        Task<List<GetJobVacanciesDBRequestDto>?> GetJobVacanciesAsync(string estado, string id);
        Task<int?> GetJobVacancyFileIdAsync(int id);
        Task<int?> SetJobVacancyAsync(SetJobVacancyDBRequestDto dbRequest);
        Task<bool?> UpdateJobVacancyAsync(UpdateJobVacancyDBRequestDto dbRequest);
    }
}
