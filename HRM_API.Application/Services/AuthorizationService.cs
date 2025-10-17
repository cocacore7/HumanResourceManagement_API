using HRM_API.Application.Helpers;
using HRM_API.Core.Interfaces.Authorization;
using System.Text;

namespace HRM_API.Application
{
    public class AuthorizationService
    {
        private readonly IAuthorizationRepository _repository;
        private readonly JwtService _jwtService;

        public AuthorizationService(IAuthorizationRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

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
