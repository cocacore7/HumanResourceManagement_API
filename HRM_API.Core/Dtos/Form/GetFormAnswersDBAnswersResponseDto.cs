namespace HRM_API.Core.Dtos.Form
{
    public class GetFormAnswersDBAnswersResponseDto
    {
        public int? IdAnswer { get; set; }
        public int? QuestionId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string AnswerType { get; set; } = string.Empty;
        public bool? ValueBool { get; set; }
        public string ValueText { get; set; } = string.Empty;
        public int? ValueNumber { get; set; }
        public DateTime? ValueDate { get; set; }
        public int? IdFile { get; set; }
        public string? FileName { get; set; } = string.Empty;
        public string? ContentType { get; set; } = string.Empty;
        public string? FilePath { get; set; } = string.Empty;
        public int? SizeBytes { get; set; }
        public int? IdOption { get; set; }
        public string? Value { get; set; } = string.Empty;
        public string? Label { get; set; } = string.Empty;
    }
}
