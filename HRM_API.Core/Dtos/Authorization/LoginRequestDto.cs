namespace HRM_API.Core.Dtos.Authorization
{
    public class LoginRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
