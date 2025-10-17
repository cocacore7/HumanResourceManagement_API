

using HRM_API.Core.Dtos.Authorization;

namespace HRM_API.Core.Interfaces.Mail
{
    public interface IMailRepository
    {
        Task<AssessmentTestDto?> GetAssessmentTestAsync(int id);
    }
}