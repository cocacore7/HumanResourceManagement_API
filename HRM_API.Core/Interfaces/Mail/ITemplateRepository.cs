public interface ITemplateAdapter
{
    string TemplateName { get; } 
    Task<string> BuildBodyAsync(int id);
}