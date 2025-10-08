namespace HRM_API.Core.Dtos.JobVacancy
{
    public class GetJobVacanciesResponseDto
    {
        public List<GetJobVacanciesDto> Response { get; set; } = new List<GetJobVacanciesDto>();
    }
}
