using HRM_API.Core.Dtos.PreApplication;

namespace HRM_API.Core.Interfaces.PreApplication
{
    public interface IPreApplicationRepository
    {
        Task<List<GetPreApplicationsDto>?> GetPreApplicationsAsync();
    }
}
