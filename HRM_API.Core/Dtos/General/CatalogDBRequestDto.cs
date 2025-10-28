namespace HRM_API.Core.Dtos.General
{
    public class CatalogDBRequestDto
    {
        public int? optionId { get; set; }
        public string Value { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}
