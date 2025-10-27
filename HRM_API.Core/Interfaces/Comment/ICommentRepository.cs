using HRM_API.Core.Dtos.Comment;

namespace HRM_API.Core.Interfaces.Comment
{
    public interface ICommentRepository
    {
        Task<List<GetCommentsDBResponseDto>?> GetCommentsAsync(int? preApplicationId, string estado);
        Task<int?> SetCommentsAsync(SetCommentsDBRequestDto request);
    }
}