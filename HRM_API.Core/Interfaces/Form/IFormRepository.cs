using HRM_API.Core.Dtos.Form;

namespace HRM_API.Core.Interfaces.Form
{
    public interface IFormRepository
    {
        Task<GetFormAnswersDBFormResponseDto?> GetFormAsync(int FormId);
        Task<GetFormAnswersDBHeaderResponseDto?> GetFormHeaderAsync(int PreApplicationId, int FormId);
        Task<List<GetFormAnswersDBAnswersResponseDto>?> GetFormAnswersAsync(int? IdResponse);
    }
}
