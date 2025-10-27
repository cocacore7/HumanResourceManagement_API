using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.Comment;
using HRM_API.Core.Interfaces.Comment;

namespace HRM_API.Application.Services
{
    public class CommentService(ICommentRepository repository)
    {
        private readonly ICommentRepository _repository = repository;

        public async Task<GetCommentsResponseDto?> GetCommentsAsync(int? preApplicationId, string estado)
        {
            var responsedb = await _repository.GetCommentsAsync(preApplicationId, estado);

            GetCommentsResponseDto response = new() { Response = responsedb ?? [] };

            return response;
        }

        public async Task<SetCommentsResponseDto?> SetCommentsAsync(SetCommentsRequestDto request, LoginDBResponseDto user)
        {
            SetCommentsDBRequestDto newComment = new() 
            {
                PreApplicationId = request.PreApplicationId,
                AuthorUserId = int.TryParse(user.IdUser, out int createdBy) ? createdBy : 0,
                CommentText = request.CommentText,
                CommentStatus = request.CommentStatus
            };
            var responsedb = await _repository.SetCommentsAsync(newComment);
            if (responsedb == null) { return null; }

            SetCommentsResponseDto response = new() { Response = responsedb};

            return response;
        }
    }
}
