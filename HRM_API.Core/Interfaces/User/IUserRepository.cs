using HRM_API.Core.Dtos.User;

namespace HRM_API.Core.Interfaces.User
{
    public interface IUserRepository
    {
        Task<List<GetUserModulesDBResponseDto>?> GetUserModulesAsync(int userId);
        Task<int?> GetUserByEmailAsync(string Email);
        Task<string?> GetEmailByUserAsync(int IdUser);
    }
}
