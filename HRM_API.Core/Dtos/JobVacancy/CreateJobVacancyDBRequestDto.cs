namespace HRM_API.Core.Dtos.JobVacancy
{
    public class CreateJobVacancyDBRequestDto
    {
        public string JobPositionName { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string RequesterPosition { get; set; } = string.Empty;
        public string AreaText { get; set;} = string.Empty;
        public string RegionText { get; set; } = string.Empty;
        public string HubText { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int VacancyTypeId { get; set; } = 0;
        public int ReasonId { get; set; } = 0;
        public decimal Salary { get; set; } = 0;
        public int TotalPositions { get; set; } = 0;
        public int AvailablePosition { get; set; } = 0;
        public int RquisitionFileId { get; set; } = 0;
        public string Status { get; set; } = string.Empty;
        public int CreatedBy { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
