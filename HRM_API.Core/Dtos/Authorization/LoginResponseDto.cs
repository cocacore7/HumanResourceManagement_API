namespace HRM_API.Core.Dtos.Authorization
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiredDate { get; set; } = DateTime.Now;
    }
}
