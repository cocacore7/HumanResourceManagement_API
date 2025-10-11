namespace HRM_API.Core.Dtos.General
{
    public class GeneralFormRequestFormDto
    {
        public int FormId { get; set; }
        public string KeyName { get; set; } = string.Empty;
        public int VersionNumber { get; set; }
    }
}
