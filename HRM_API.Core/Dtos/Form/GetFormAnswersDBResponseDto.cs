namespace HRM_API.Core.Dtos.Form
{
    public class GetFormAnswersDBResponseDto
    {
        public GetFormAnswersDBFormResponseDto Form { get; set; } = new();
        public GetFormAnswersDBHeaderResponseDto Header { get; set; } = new();
        public List<GetFormAnswersDBAnswersResponseDto> Answers { get; set; } = new();
    }
}
