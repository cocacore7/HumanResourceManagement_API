using HRM_API.Core.Dtos.User;
using HRM_API.Core.Interfaces.User;

namespace HRM_API.Application.Services
{
    public class UserService(IUserRepository repository)
    {
        private readonly IUserRepository _repository = repository;

        public async Task<GetUserModulesResponseDto?> GetUserModulesAsync(string userId)
        {
            int userIdSP = int.TryParse(userId, out int createdBy) ? createdBy : 0;
            var form = await _repository.GetUserModulesAsync(userIdSP);
            GetUserModulesResponseDto response = new() { Response = form ?? [] };

            return (response);
        }
    }
}
