using System.Text;
using HRM_API.Core.Interfaces.Mail;

namespace HRM_API.Application.Templates
{
    public class EndProcessAdapter : ITemplateAdapter
    {
        private readonly IMailRepository _repository;

        public string TemplateName => "EndProcesss.html";

        public EndProcessdapter(IMailRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> EndProcessAsync(int id)
        {
            var dto = await _repository.GetEndProcessAsync(id);
            if (dto == null)
                throw new Exception($"No se encontró la prueba con id={id}");

            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", TemplateName);
            var htmlBody = await File.ReadAllTextAsync(templatePath, Encoding.UTF8);

            htmlBody = htmlBody
                .Replace("[NOMBRE COMPLETO]", dto.FullName)
                .Replace("[ACTION]", dto.Action)

            return htmlBody;
        }
    }
}
