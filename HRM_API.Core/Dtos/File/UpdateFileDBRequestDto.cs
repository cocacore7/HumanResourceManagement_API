namespace HRM_API.Core.Dtos.File
{
    public class UpdateFileDBRequestDto
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public long SizeBytes { get; set; } = 0;
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        public int? IdFile { get; set; }
    }
}
