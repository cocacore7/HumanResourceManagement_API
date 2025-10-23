namespace HRM_API.Core.Dtos.JobVacancy
{
    public class UpdateJobVacancyDBRequestDto
    {
        public string JobPositionName { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string RequesterPosition { get; set; } = string.Empty;
        public string AreaText { get; set; } = string.Empty;
        public string RegionText { get; set; } = string.Empty;
        public string HubText { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int VacancyTypeId { get; set; } = 0;
        public int ReasonId { get; set; } = 0;
        public decimal Salary { get; set; } = 0;
        public decimal TotalPositions { get; set; } = 0;
        public decimal AvailablePositions { get; set; } = 0;
        public string Status { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int? IdVacancy { get; set; }
    }
}
