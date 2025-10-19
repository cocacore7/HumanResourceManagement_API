using HRM_API.Core.Dtos.Form;

namespace HRM_API.Core.Interfaces.Form
{
    public interface IFormRepository
    {
        Task<GetFormAnswersDBFormResponseDto?> GetFormAsync(int FormId);
        Task<GetFormAnswersDBHeaderResponseDto?> GetFormHeaderAsync(int PreApplicationId, int FormId);
        Task<List<GetFormAnswersDBAnswersResponseDto>?> GetFormAnswersAsync(int? IdResponse);
        Task<GetPreApplicationFormResponseDBResponseDto?> GetPreApplicationFormResponseAsync(int? PreApplicationId, int? FormId);
        Task<GetValidQuestionDBResponseDto?> GetValidQuestionAsync(int? FormId, string Code);
        Task<int?> GetQuestionOptionAsync(int? QuestionId, int? CatalogId);
        Task<int?> SetPreApplicationFormResponseAsync(SetPreApplicationFormResponseDBRequestDto PreApplicationForm);
        Task<bool?> SetTextAnswerAsync(SetTextAnswerDBResponseDto PreApplicationForm);
        Task<bool?> SetNumberAnswerAsync(SetNumberAnswerDBResponseDto PreApplicationForm);
        Task<bool?> SetBoolAnswerAsync(SetBoolAnswerDBResponseDto PreApplicationForm);
        Task<bool?> SetFileAnswerAsync(SetFileAnswerDBResponseDto PreApplicationForm);
        Task<bool?> SetDateAnswerAsync(SetDateAnswerDBResponseDto PreApplicationForm);
        Task<int?> SetEnumAnswerAsync(SetEnumAnswerDBResponseDto PreApplicationForm);
        Task<bool?> SetEnumAnswerOptionAsync(int? AnswerId, int? OptionId);
    }
}
