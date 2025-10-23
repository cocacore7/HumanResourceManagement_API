namespace HRM_API.Core.Dtos.PreApplication
{
    public class SetPreApplicationsDBRequestDto
    {
        public string FullName { get; set; } = string.Empty;
        public string DPI { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? TownId { get; set; }
        public string Address { get; set; } = string.Empty;
        public string EducationLevel { get; set; } = string.Empty;
        public int? VacancyId { get; set; }
        public bool? Experience { get; set; }
        public string HowHeard { get; set; } = string.Empty;
        public int? CVFileId { get; set; }
        public bool? AcceptedTerms { get; set; }
        public string Origin { get; set; } = string.Empty;
        public string Status { get; set; } = "preSolicitud";
        public bool IsReferred { get; set; } = false;
        public string RefferedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? CreatedBy { get; set; }
    }
}
