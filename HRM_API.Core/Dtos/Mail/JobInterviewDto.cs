namespace HRM_API.Core.Dtos.Authorization
{
    public class JobInterviewDto
    {
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; } = 0;
        public string Gender { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}