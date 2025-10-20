using System.Text;
using HRM_API.Core.Interfaces.Mail;

namespace HRM_API.Application.Templates
{
    public class PreScreeningAdapter : ITemplateAdapter
    {
        private readonly IMailRepository _repository;

        public string TemplateName => "precalificacion.html";

        public PreScreeningAdapter(IMailRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> BuildBodyAsync(int id)
        {
            var dto = await _repository.GetPreScreeningAsync(id);
            if (dto == null)
                throw new Exception($"No se encontró la prueba con id={id}");

            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", TemplateName);
            var htmlBody = await File.ReadAllTextAsync(templatePath, Encoding.UTF8);

            htmlBody = htmlBody
                .Replace("[CODE]", dto.Code.ToString())
                .Replace("[FULLNAME]", dto.FullName)
                .Replace("[AGE]", dto.Age.ToString())
                .Replace("[GENDER]", dto.Gender)
                .Replace("[PHONE]", dto.Phone)
                .Replace("[EMAIL]", dto.Email)
                .Replace("[ACTION]", dto.Action)
                .Replace("[DATEAT]", DateTime.Parse(dto.CreatedAt).ToString("dd/MM/yyyy"))
                .Replace("[NOTE]", dto.Note ?? "");

            return htmlBody;
        }
    }
}
