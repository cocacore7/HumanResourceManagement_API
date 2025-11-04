using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.User;

namespace HRM_API.Core.Interfaces.User
{
    public interface IUserRepository
    {
        Task<List<GetUserModulesDBResponseDto>?> GetUserModulesAsync(int userId);
        Task<GetUserByEmailDBResponseDto?> GetUserByEmailAsync(string Email);
        Task<string?> GetActualUserEmailAsync(int IdPreApplication);
        Task<string?> GetEmailByUserAsync(int IdUser);
        Task<LoginDBResponseDto?> GetPublicUserAsync();
    }
}
