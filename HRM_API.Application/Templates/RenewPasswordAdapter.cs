using HRM_API.Configuration;
using HRM_API.Core.Interfaces.Mail;
using System.Text;

namespace HRM_API.Application.Templates
{
    public class RenewPasswordAdapter(IMailRepository repository, ISettings settings) : ITemplateRepository
    {
        private readonly IMailRepository _repository = repository;
        private readonly ISettings _settings = settings;

        public string TemplateName => "RenewPassword.html";

        public async Task<string> BuildBodyAsync(string fullname, string renewPassword)
        {
            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", TemplateName);
            var htmlBody = await File.ReadAllTextAsync(templatePath, Encoding.UTF8);

            htmlBody = htmlBody
                .Replace("[FULLNAME]", fullname)
                .Replace("[PASSWORD]", renewPassword);
                .Replace("[RECRUITER_URL]", _settings.RecruiterUrl ?? "");
            return htmlBody;
        }
    }
}
