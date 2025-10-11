namespace HRM_API.Core.Dtos.General
{
    public class GeneralFormRequestDto
    {
        public GeneralFormRequestFormDto? Form { get; set; }
        public GeneralFormRequestOriginDto? Origin { get; set; }
        public List<GeneralFormRequestAnswerDto>? Answers { get; set; }
    }
}
