namespace HRM_API.Core.Dtos.Authorization
{
    public class GenerateNewPasswordDBRequestDto
    {
        public int UserId { get; set; }
        public required byte[] NewPassword { get; set; }
    }
}
