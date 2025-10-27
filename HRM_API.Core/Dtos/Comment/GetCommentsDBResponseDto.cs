namespace HRM_API.Core.Dtos.Comment
{
    public class GetCommentsDBResponseDto
    {
        public int? IdComment { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
