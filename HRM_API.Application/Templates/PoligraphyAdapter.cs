using HRM_API.Configuration;
using HRM_API.Core.Interfaces.Mail;
using System.Text;

namespace HRM_API.Application.Templates
{
    public class PolygraphAdapter(IMailRepository repository, ISettings settings) : ITemplateRepository
    {
        private readonly IMailRepository _repository = repository;
        private readonly ISettings _settings = settings;

        public string TemplateName => "Poligraphy.html";

        public async Task<string> BuildBodyAsync(int id)
        {
            var dto = await _repository.GetPolygraphAsync(id) ?? throw new Exception($"No se encontró la prueba con id={id}");
            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", TemplateName);
            var htmlBody = await File.ReadAllTextAsync(templatePath, Encoding.UTF8);

            htmlBody = htmlBody
                .Replace("[FULLNAME_RECRUITER]", dto.FullName_Recruiter)
                .Replace("[CODE]", dto.Code.ToString())
                .Replace("[FULLNAME]", dto.FullName)
                .Replace("[AGE]", dto.Age.ToString())
                .Replace("[GENDER]", dto.Gender)
                .Replace("[PHONE]", dto.Phone)
                .Replace("[EMAIL]", dto.Email)
                .Replace("[JOBPOSITIONNAME]", dto.JobPositionName)
                .Replace("[DATEAT]", DateTime.ParseExact(dto.CreatedAt, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"))
                .Replace("[COMMENT]", dto.Comment ?? "")
                .Replace("[RECRUITER_URL]", _settings.RecruiterUrl ?? "");

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
