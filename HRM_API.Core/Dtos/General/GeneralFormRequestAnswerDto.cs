using System.ComponentModel.DataAnnotations;

namespace HRM_API.Core.Dtos.General
{
    public class GeneralFormRequestAnswerDto
    {
        // Campos obligatorios
        [Required]
        public int QuestionId { get; set; }

        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        // Campos opcionales según el tipo de pregunta
        public string? ValueText { get; set; }
        public decimal? ValueNumber { get; set; }
        public int? OptionId { get; set; }
        public string? OptionValue { get; set; }
        public bool? ValueBool { get; set; }
        public string ValueDate { get; set; } = string.Empty;

        // Datos de archivo (solo si type = "file")
        public int? IdFile { get; set; }
        public string? FileCode { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public string? FileType { get; set; }
        public long? SizeBytes { get; set; }
        public long? FileSize { get; set; }
        public string? Base64 { get; set; }
    }
}
