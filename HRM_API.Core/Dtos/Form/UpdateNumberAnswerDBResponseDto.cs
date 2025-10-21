namespace HRM_API.Core.Dtos.Form
{
    public class UpdateNumberAnswerDBResponseDto
    {
        public string AnswerType { get; set; } = string.Empty;
        public decimal? ValueNumber { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int? ResponseId { get; set; }
        public int? QuestionId { get; set; }
    }
}
