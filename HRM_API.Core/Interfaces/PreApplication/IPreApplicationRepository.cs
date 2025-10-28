using HRM_API.Core.Dtos.General;
using HRM_API.Core.Dtos.PreApplication;

namespace HRM_API.Core.Interfaces.PreApplication
{
    public interface IPreApplicationRepository
    {
        Task<List<GetPreApplicationsDBResponseDto>?> GetPreApplicationsAsync(string estado, string id);
        Task<List<GeneralFormRequestAnswerDto>?> GetPreApplicationFormsFilesAsync(int? id);
        Task<List<GeneralFormRequestAnswerDto>?> GetPreApplicationPreApplicationFileAsync(int? id);
        Task<int?> GetPreApplicationFileIdAsync(int id);
        Task<int?> SetPreApplicationsAsync(SetPreApplicationsDBRequestDto request);
        Task<bool?> UpdatePreApplicationsAsync(UpdatePreApplicationsDBRequestDto request);
        Task<bool?> UpdateIsDocumentedAsync(int? PreApplicationId, bool? IsDocumented);
        Task<bool?> UpdateStatusAssignToAsync(int? PreApplicationId, string? Status, int? AssignTo);
        Task<bool?> UpdateStatusFailAsync(int? PreApplicationId, string? Status);
    }
}
