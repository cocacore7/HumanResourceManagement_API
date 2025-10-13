using HRM_API.Core.Dtos.Form;

namespace HRM_API.Core.Interfaces.Form
{
    public interface IFormRepository
    {
        Task<List<GetFormAnswersDBFormResponseDto>?> GetFormAsync(int FormId);
        Task<List<GetFormAnswersDBHeaderResponseDto>?> GetFormHeaderAsync(int PreApplicationId, int FormId);
        Task<List<GetFormAnswersDBAnswersResponseDto>?> GetFormAnswersAsync(int PreApplicationId, int FormId);
    }
}
