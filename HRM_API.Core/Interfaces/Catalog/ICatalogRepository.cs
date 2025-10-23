using HRM_API.Core.Dtos.General;

namespace HRM_API.Core.Interfaces.Catalog
{
    public interface ICatalogRepository
    {
        Task<List<CatalogDBRequestDto>> GetJobCatalogAsync();
        Task<List<CatalogDBRequestDto>> GetTownCatalogAsync();
        Task<List<CatalogDBRequestDto>> GetTermsAndConditionsCatalogAsync();
        Task<List<CatalogDBRequestDto>> GetVacancyReasonCatalogAsync();
        Task<List<CatalogDBRequestDto>> GetVacancyTypeCatalogAsync();
        Task<List<CatalogUserDBResponse>> GetUsersCatalogAsync(string KeyName);
        Task<List<CatalogDBRequestDto>> GetRoleCatalogAsync();
    }
}
