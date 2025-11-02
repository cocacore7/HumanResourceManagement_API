using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Dtos.PreApplication;

namespace HRM_API.Core.Interfaces.PreApplication
{
    public interface IPreApplicationRepository
    {
        Task<List<GetPreApplicationsDBResponseDto>?> GetPreApplicationsAsync(string estado, string id);
        Task<List<GetPreApplicationsDBResponseDto>?> GetPreApplicationsByUserAsync(string estado, string id, int userId);
        Task<List<GetFormAnswersDBAnswersResponseDto>?> GetPreApplicationFormsFilesAsync(int? id);
        Task<GetCountPreApplicationByStateDBResponseDto?> GetCountPreApplicationByStateAsync(string state);
        Task<GetCountPreApplicationByStateDBResponseDto?> GetHiredCountPreApplicationAsync();
        Task<GetCountPreApplicationByStateDBResponseDto?> GetFailCountPreApplicationAsync();
        Task<GetCountPreApplicationByStateDBResponseDto?> GetProcessCountPreApplicationAsync();
        Task<List<GetFormAnswersDBAnswersResponseDto>?> GetPreApplicationPreApplicationFileAsync(int? id);
        Task<int?> GetPreApplicationFileIdAsync(int id);
        Task<bool?> PreAppValidatePublicAsync(long? dpi, int? id);
        Task<int?> SetPreApplicationsAsync(SetPreApplicationsDBRequestDto request);
        Task<bool?> UpdatePreApplicationsAsync(UpdatePreApplicationsDBRequestDto request);
        Task<bool?> UpdateIsDocumentedAsync(int? PreApplicationId, bool? IsDocumented);
        Task<bool?> UpdateStatusAssignToAsync(int? PreApplicationId, string? Status, int? AssignTo);
        Task<bool?> UpdateStatusFailAsync(int? PreApplicationId, string? Status);
        Task<bool?> AssignToAsync(int? PreapplicationId, int? IdUserAssign);
    }
}
