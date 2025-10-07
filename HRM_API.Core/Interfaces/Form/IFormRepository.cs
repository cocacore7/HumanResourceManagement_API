using HRM_API.Core.Dtos.Form;

namespace HRM_API.Core.Interfaces.Form
{
    public interface IFormRepository
    {
        Task<List<ModuleDto>?> GetUserModulesAsync(int userId);
    }
}
