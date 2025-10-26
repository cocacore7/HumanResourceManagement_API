using HRM_API.Core.Dtos.Authorization;

namespace HRM_API.Core.Interfaces.Authorization
{
    public interface IAuthorizationRepository
    {
        Task<LoginDBResponseDto?> GetUserByCredentialsAsync(string name, byte[] Password, string role);
        Task<int?> SetLoginAttemptAsync(SetLoginAttemptDBRequestDto requestdb);
        Task<GenerateNewPasswordDBResponseDto?> GenerateNewPasswordAsync(GenerateNewPasswordDBRequestDto requestdb);
        Task<int?> ValidPasswordCodeAsync(int code, int userId);
        Task<bool?> UpdatePasswordCodeAsync(int userId);
    }
}
