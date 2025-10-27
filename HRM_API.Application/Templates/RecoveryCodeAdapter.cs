using HRM_API.Core.Interfaces.Mail;
using System.Text;

namespace HRM_API.Application.Templates
{
    public class RecoveryCodeAdapter() : ITemplateRepository
    {
        public string TemplateName => "RecoveryCode.html";

        public async Task<string> BuildBodyAsync(int id)
        {
            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", TemplateName);
            await File.ReadAllTextAsync(templatePath, Encoding.UTF8);
            return "";
        }

        public async Task<string> BuildBodyPasswordAsync(string fullname, string recoverycode)
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
