namespace HRM_API.Configuration
{
    public class Settings : ISettings
    {
        public string BasePath { get; set; } = string.Empty;
        public string EmailKey { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string SmtHost { get; set; } = string.Empty;
        public int? SmtPort { get; set; }
    }
}
