using HRM_API.Core.Dtos.Authorization;

namespace HRM_API.Core.Interfaces.Authorization
{
    public interface IAuthorizationRepository
    {
        Task<LoginDto?> GetUserByCredentialsAsync(string name, byte[] Password, string role);
    }
}
