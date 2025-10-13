namespace HRM_API.Core.Dtos.Form
{
    public class GetFormAnswersDBResponseDto
    {
        public List<GetFormAnswersDBFormResponseDto> Form { get; set; } = new();
        public List<GetFormAnswersDBHeaderResponseDto> Header { get; set; } = new();
        public List<GetFormAnswersDBAnswersResponseDto> Answers { get; set; } = new();
    }
}
