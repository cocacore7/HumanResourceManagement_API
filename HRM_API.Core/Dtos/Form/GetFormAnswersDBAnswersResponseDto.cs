namespace HRM_API.Core.Dtos.Form
{
    public class GetFormAnswersDBAnswersResponseDto
    {
        public int? IdAnswer { get; set; }
        public int? QuestionId { get; set; }
        public DateTime Code { get; set; } = DateTime.Now;
        public DateTime AnswerType { get; set; } = DateTime.Now;
        public DateTime ValueBool { get; set; } = DateTime.Now;
    }
}
