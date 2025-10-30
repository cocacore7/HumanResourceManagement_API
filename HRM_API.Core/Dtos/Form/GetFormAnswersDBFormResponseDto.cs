namespace HRM_API.Core.Dtos.Form
{
    public class GetFormAnswersDBFormResponseDto
    {
        public int? IdForm { get; set; }
        public string KeyName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string VersionNumber { get; set; } = string.Empty;
    }
}
