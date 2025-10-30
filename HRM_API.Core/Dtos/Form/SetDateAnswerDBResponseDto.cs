namespace HRM_API.Core.Dtos.Form
{
    public class SetDateAnswerDBResponseDto
    {
        public int? ResponseId { get; set; }
        public int? QuestionId { get; set; }
        public string AnswerType { get; set; } = string.Empty;
        public string? ValueDate { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
