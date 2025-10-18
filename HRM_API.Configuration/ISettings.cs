namespace HRM_API.Configuration
{
    public interface ISettings
    {
        string BasePath { get; set; }
        string EmailKey { get; set; }
        string FromAddress { get; set; }
        string EmailAddress { get; set; }
        string SmtHost { get; set; }
        int? SmtPort { get; set; }
    }
}
