using System.Text;
using HRM_API.Core.Interfaces.Mail;

namespace HRM_API.Application.Templates
{
    public class RecordAdapter : ITemplateRepository
    {
        private readonly IMailRepository _repository;

        public string TemplateName => "Record.html";

        public RecordAdapter(IMailRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> BuildBodyAsync(int id)
        {
            var dto = await _repository.GetRecordAsync(id);
            if (dto == null)
                throw new Exception($"No se encontró la prueba con id={id}");

            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", TemplateName);
            var htmlBody = await File.ReadAllTextAsync(templatePath, Encoding.UTF8);

            htmlBody = htmlBody
                .Replace("[FULLNAME_RECRUITER]", dto.Code.ToString())
                .Replace("[FULLNAME]", dto.FullName)
                .Replace("[DPI]", dto.DPI)
                .Replace("[JOBPOSITIONNAME]", dto.JobPositionName);

            return htmlBody;
        }
    }
}
