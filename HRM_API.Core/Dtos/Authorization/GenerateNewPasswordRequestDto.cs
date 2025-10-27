namespace HRM_API.Core.Dtos.Authorization
{
    public class GenerateNewPasswordRequestDto
    { 
        public string Email { get; set; } = string.Empty;
        public int RecoveryCode { get; set; }
    }
}
