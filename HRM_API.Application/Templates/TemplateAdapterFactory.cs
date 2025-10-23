using HRM_API.Core.Interfaces.Mail;

public class TemplateAdapterFactory
{
    private readonly Dictionary<string, ITemplateRepository> _adapters;

    public TemplateAdapterFactory(IEnumerable<ITemplateRepository> adapters)
    {
        _adapters = adapters.ToDictionary(a => Path.GetFileNameWithoutExtension(a.TemplateName),
                                          a => a, StringComparer.OrdinalIgnoreCase);
    }

    public ITemplateRepository GetAdapter(string templateName)
    {
        if (!_adapters.TryGetValue(templateName, out var adapter))
            throw new KeyNotFoundException($"No existe adaptador para la plantilla '{templateName}'.");

        return adapter;
    }
}
