namespace HRM_API.Core.Dtos.Form
{
    public class UpdateTextAnswerDBResponseDto
    {
        public string AnswerType { get; set; } = string.Empty;
        public string ValueText { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int? ResponseId { get; set; }
        public int? QuestionId { get; set; }
    }
}
