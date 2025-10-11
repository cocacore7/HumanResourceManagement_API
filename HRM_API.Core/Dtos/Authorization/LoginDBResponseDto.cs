namespace HRM_API.Core.Dtos.Authorization
{
    public class LoginDBResponseDto
    {
        public string IdUser { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string RoleId { get; set; } = string.Empty;
    }
}
