namespace HRM_API.Core.Dtos.Form
{
    public class UpdateBoolAnswerDBResponseDto
    {
        public int? ResponseId { get; set; }
        public int? QuestionId { get; set; }
        public string AnswerType { get; set; } = string.Empty;
        public bool? ValueBool { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
