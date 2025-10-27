using System.Text;
using HRM_API.Core.Interfaces.Mail;

namespace HRM_API.Application.Templates
{
    public class EndProcessAdapter(IMailRepository repository) : ITemplateRepository
    {
        private readonly IMailRepository _repository = repository;

        public string TemplateName => "EndProcess.html";

        public async Task<string> BuildBodyAsync(int id)
        {
            var dto = await _repository.GetEndProcessAsync(id) ?? throw new Exception($"No se encontró la prueba con id={id}");
            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", TemplateName);
            var htmlBody = await File.ReadAllTextAsync(templatePath, Encoding.UTF8);

            htmlBody = htmlBody
                .Replace("[FULLNAME]", dto.FullName)
                .Replace("[JOBPOSITIONNAME]", dto.JobPositionName);

            return htmlBody;
        }

        public async Task<string> BuildBodyPasswordAsync(string fullname, string recoverycode)
        {
            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", TemplateName);
            await File.ReadAllTextAsync(templatePath, Encoding.UTF8);

            return "";
        }
    }
}
