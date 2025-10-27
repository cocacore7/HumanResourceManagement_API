namespace HRM_API.Core.Interfaces.Mail
{
    public interface ITemplateRepository
    {
        string TemplateName { get; }
        Task<string> BuildBodyAsync(int id);
        Task<string> BuildBodyPasswordAsync(string fullname, string recoverycode);
    }
}