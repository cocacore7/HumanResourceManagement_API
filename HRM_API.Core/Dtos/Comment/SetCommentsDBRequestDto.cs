namespace HRM_API.Core.Dtos.Comment
{
    public class SetCommentsDBRequestDto
    {
        public int? PreApplicationId { get; set; }
        public int? AuthorUserId { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public string CommentStatus { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
