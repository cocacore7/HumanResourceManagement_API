namespace HRM_API.Core.Dtos.User
{
    public class GetUserByEmailDBResponseDto
    {
        public int? IdUser { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
