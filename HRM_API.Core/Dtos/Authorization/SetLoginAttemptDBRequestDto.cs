namespace HRM_API.Core.Dtos.Authorization
{
    public class SetLoginAttemptDBRequestDto
    {
        public int? UserId { get; set; }
        public string EmailEntered { get; set; } = string.Empty;
        public bool? Success { get; set; }
        public int? RecoveryCode { get; set; }
        public bool? RecoveryCodeUsed { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
