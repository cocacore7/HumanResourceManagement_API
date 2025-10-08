namespace HRM_API.Core.Dtos.PreApplication
{
    public class GetPreApplicationsDto
    {
        public int IdModule { get; set; } = 0;
        public string Path { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}
