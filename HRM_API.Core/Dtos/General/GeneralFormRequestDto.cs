namespace HRM_API.Core.Dtos.General
{
    public class GeneralFormRequestDto
    {
        public GeneralFormRequestFormDto Form { get; set; } = new();
        public GeneralFormRequestOriginDto Origin { get; set; } = new();
        public List<GeneralFormRequestAnswerDto>? Answers { get; set; }
    }
}
