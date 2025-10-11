namespace HRM_API.Core.Dtos.General
{
    public class GeneralFormRequestAnswerDto
    {
        public int QuestionId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        // Campos posibles según el tipo de pregunta
        public string? ValueText { get; set; }
        public decimal? ValueNumber { get; set; }
        public int? OptionId { get; set; }
        public string? OptionValue { get; set; }

        // Datos de archivo (solo si type = "file")
        public string? FileCode { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public string? FileType { get; set; }
        public long? SizeBytes { get; set; }
        public long? FileSize { get; set; }
        public string? Base64 { get; set; }
    }
}
