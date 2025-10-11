using HRM_API.Application.Helpers;
using HRM_API.Core.Interfaces.Authorization;

namespace HRM_API.Application.Services
{
    public class AuthorizationService(IAuthorizationRepository repository, JwtService jwtService)
    {
        private readonly IAuthorizationRepository _repository = repository;
        private readonly JwtService _jwtService = jwtService;

        public async Task<string?> AuthenticateAsync(string name, string Password, string role)
        {
            var bytes = System.Text.Encoding.ASCII.GetBytes(Password);
            var user = await _repository.GetUserByCredentialsAsync(name, bytes, role);

            if (user == null)
                return null;

            return _jwtService.GenerateToken(user.IdUser, user.Name, user.RoleId);
        }
    }
}
