public class TemplateAdapterFactory
{
    private readonly Dictionary<string, ITemplateAdapter> _adapters;

    public TemplateAdapterFactory(IEnumerable<ITemplateAdapter> adapters)
    {
        _adapters = adapters.ToDictionary(a => Path.GetFileNameWithoutExtension(a.TemplateName),
                                          a => a, StringComparer.OrdinalIgnoreCase);
    }

    public ITemplateAdapter GetAdapter(string templateName)
    {
        if (!_adapters.TryGetValue(templateName, out var adapter))
            throw new KeyNotFoundException($"No existe adaptador para la plantilla '{templateName}'.");

        return adapter;
    }
}
