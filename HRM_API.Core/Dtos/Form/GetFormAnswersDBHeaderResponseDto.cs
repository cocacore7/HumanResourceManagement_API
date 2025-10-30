namespace HRM_API.Core.Dtos.Form
{
    public class GetFormAnswersDBHeaderResponseDto
    {
        public int? IdResponse { get; set; }
        public int? PreApplicationId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
