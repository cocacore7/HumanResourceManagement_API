

using HRM_API.Core.Dtos.Authorization;

namespace HRM_API.Core.Interfaces.Mail
{
    public interface IAssessmentRepository
    {
        Task<AssessmentTestDto?> GetAssessmentTestAsync(int id);
    }
}