

using HRM_API.Core.Dtos.Authorization;

namespace HRM_API.Core.Interfaces.Mail
{
    public interface IMailRepository
    {
        Task<AssessmentTestDto?>  GetAssessmentTestAsync(int id);
        Task<BossInterviewDto?>   GetBossInterviewAsync(int id);
        Task<EndProcessDto?>      GetEndProcessAsync(int id);
        Task<JobInterviewDto?>    GetJobInterviewAsync(int id);
        Task<PolygraphDto?>       GetPolygraphAsync(int id);
        Task<PreScreeningDto?>    GetPreScreeningAsync(int id);
        Task<RecordDto?>          GetRecordAsync(int id);
        Task<CandidateRecordDto?> GetCandidateRecordAsync(int id);
    }
}