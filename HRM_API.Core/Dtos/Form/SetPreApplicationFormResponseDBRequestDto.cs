namespace HRM_API.Core.Dtos.Form
{
    public class SetPreApplicationFormResponseDBRequestDto
    {
        public int? PreApplicationId { get; set; }
        public int? FormId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }
    }
}
