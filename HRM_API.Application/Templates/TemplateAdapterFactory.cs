using HRM_API.Core.Interfaces.Mail;
namespace HRM_API.Application.Templates
{
    public class TemplateAdapterFactory(IEnumerable<ITemplateRepository> adapters)
    {
        private readonly Dictionary<string, ITemplateRepository> _adapters = adapters.ToDictionary(a => Path.GetFileNameWithoutExtension(a.TemplateName),
                                              a => a, StringComparer.OrdinalIgnoreCase);

        public ITemplateRepository GetAdapter(string templateName)
        {
            if (!_adapters.TryGetValue(templateName, out var adapter))
                throw new KeyNotFoundException($"No existe adaptador para la plantilla '{templateName}'.");

            return adapter;
        }
    }
}