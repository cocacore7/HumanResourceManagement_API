using System.Text;
using HRM_API.Core.Interfaces.Mail;

namespace HRM_API.Application.Templates
{
    public class CandidateRecordAdapter : ITemplateAdapter
    {
        private readonly IMailRepository _repository;

        public string TemplateName => "CandidateRecord.html";

        public CandidateRecordAdapter(IMailRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> CandidateRecordAsync(int id)
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

            return htmlBody;
        }
    }
}
