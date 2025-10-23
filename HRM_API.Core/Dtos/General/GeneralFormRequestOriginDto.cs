namespace HRM_API.Core.Dtos.General
{
    public class GeneralFormRequestOriginDto
    {
        public string Description { get; set; } = string.Empty;
        public int RegisterId { get; set; } = 0;
        public string State { get; set; } = string.Empty;
        public bool IsDocumented { get; set; } = false;
    }
}
