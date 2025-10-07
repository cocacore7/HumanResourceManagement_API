namespace HRM_API.Core.Dtos.Form
{
    public class ModuleDto
    {
        public int IdModule { get; set; } = 0;
        public string Path {  get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}
