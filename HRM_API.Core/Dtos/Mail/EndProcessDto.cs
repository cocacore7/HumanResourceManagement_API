namespace HRM_API.Core.Dtos.Authorization
{
    public class EndProcessDto
    {
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
    }
}