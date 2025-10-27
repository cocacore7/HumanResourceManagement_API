using System.Text;
using HRM_API.Configuration;
using HRM_API.Core.Interfaces.Mail;

namespace HRM_API.Application.Templates
{
    public class CandidateRecordAdapter(IMailRepository repository, ISettings settings) : ITemplateRepository
    {
        private readonly IMailRepository _repository = repository;
        private readonly ISettings _settings = settings;

        public string TemplateName => "CandidateRecord.html";

        public async Task<string> BuildBodyAsync(int id)
        {
            var dto = await _repository.GetCandidateRecordAsync(id);
            if (dto == null)
                throw new Exception($"No se encontró la prueba con id={id}");

            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", TemplateName);
            var htmlBody = await File.ReadAllTextAsync(templatePath, Encoding.UTF8);

            htmlBody = htmlBody
                .Replace("[CODE]", dto.Code.ToString())
                .Replace("[FULLNAME]", dto.FullName)
                .Replace("[JOBPOSITIONNAME]", dto.JobPositionName)
                .Replace("[CANDIDATE_URL]", _settings.CandidateUrl ?? "");

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
