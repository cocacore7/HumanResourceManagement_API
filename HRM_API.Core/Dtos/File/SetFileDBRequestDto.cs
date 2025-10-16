namespace HRM_API.Core.Dtos.File
{
    public class SetFileDBRequestDto
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public long SizeBytes { get; set; } = 0;
        public int UploadedBy { get; set; } = 0;
        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}
