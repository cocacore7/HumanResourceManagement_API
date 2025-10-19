namespace HRM_API.Core.Dtos.Form
{
    public class SetDateAnswerDBResponseDto
    {
        public int? ResponseId { get; set; }
        public int? QuestionId { get; set; }
        public string AnswerType { get; set; } = string.Empty;
        public DateOnly ValueDate { get; set; } = DateOnly.FromDateTime(new DateTime());
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
