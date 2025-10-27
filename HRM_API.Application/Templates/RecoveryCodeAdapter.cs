using HRM_API.Configuration;
using HRM_API.Core.Interfaces.Mail;
using System.Text;

namespace HRM_API.Application.Templates
{
    public class RecoveryCodeAdapter(IMailRepository repository, ISettings settings) : ITemplateRepository
    {
        private readonly IMailRepository _repository = repository;
        private readonly ISettings _settings = settings;

        public string TemplateName => "RecoveryCode.html";

        public async Task<string> BuildBodyAsync(string fullname, string recoverycode)
        {
            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", TemplateName);
            var htmlBody = await File.ReadAllTextAsync(templatePath, Encoding.UTF8);

            htmlBody = htmlBody
                .Replace("[FULLNAME]", fullname)
                .Replace("[RECOVERY_CODE]", recoverycode);

            return htmlBody;
        }
    }
}
