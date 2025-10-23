namespace HRM_API.Core.Dtos.Mail
{
    public class PreScreeningDto
    {
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string FullName_Recruiter { get; set; } = string.Empty;
        public int Age { get; set; } =  0;
        public string Gender { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string JobPositionName { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
    }
}