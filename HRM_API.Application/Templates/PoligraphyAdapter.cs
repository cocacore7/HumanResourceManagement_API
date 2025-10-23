using System.Text;
using HRM_API.Core.Interfaces.Mail;

namespace HRM_API.Application.Templates
{
    public class PolygraphAdapter : ITemplateRepository
    {
        private readonly IMailRepository _repository;

        public string TemplateName => "poligrafo.html";

        public PolygraphAdapter(IMailRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> BuildBodyAsync(int id)
        {
            var dto = await _repository.GetPolygraphAsync(id);
            if (dto == null)
                throw new Exception($"No se encontró la prueba con id={id}");

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
                .Replace("[JOBPOSITIONAME]", dto.JobPositionName)
                .Replace("[DATEAT]", DateTime.Parse(dto.CreatedAt).ToString("dd/MM/yyyy"))
                .Replace("[COMMENT]", dto.Comment ?? "");

            return htmlBody;
        }
    }
}
