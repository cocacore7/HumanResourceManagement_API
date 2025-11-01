namespace HRM_API.Core.Dtos.Authorization
{
    public class RefreshTokenRequestDto
    {
        public string OldToken { get; set; } = string.Empty;
    }
}
