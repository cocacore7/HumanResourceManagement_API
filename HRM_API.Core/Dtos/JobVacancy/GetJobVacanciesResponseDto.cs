namespace HRM_API.Core.Dtos.JobVacancy
{
    public class GetJobVacanciesResponseDto
    {
        public List<GetJobVacanciesDBRequestDto> Response { get; set; } = new List<GetJobVacanciesDBRequestDto>();
    }
}
