namespace HRM_API.Core.Dtos.Comment
{
    public class SetCommentsRequestDto
    {
        public int? PreApplicationId { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public string CommentStatus { get; set; } = string.Empty;
    }
}
