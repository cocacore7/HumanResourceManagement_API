namespace HRM_API.Core.Dtos.File
{
    public class SetFileDBRequestDto
    {
        public string fileName { get; set; } = string.Empty;
        public string contentType { get; set; } = string.Empty;
        public string filePath { get; set; } = string.Empty;
        public long sizeBytes { get; set; } = 0;
        public int uploadedBy { get; set; } = 0;
        public DateTime uploadedAt { get; set; } = DateTime.Now;
    }
}
