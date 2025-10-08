namespace HRM_API.Core.Dtos.Form
{
    public class GetUserModulesDto
    {
        public int IdModule { get; set; } = 0;
        public string Path {  get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}
