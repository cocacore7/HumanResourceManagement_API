namespace HRM_API.Core.Dtos.Authorization
{
    public class RecordDto
    {
        public string Code { get; set; } = string.Empty;
        public string FullName_Recruiter { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string DPI { get; set; } = string.Empty;
        public string JobPositionName { get; set; } = string.Empty;
    }
}