using HRM_API.Application.Helpers;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Interfaces.Authorization;
using HRM_API.Core.Interfaces.User;

namespace HRM_API.Application.Services
{
    public class AuthorizationService(IAuthorizationRepository repository, IUserRepository userRepository, JwtService jwtService)
    {
        private readonly IAuthorizationRepository _repository = repository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly JwtService _jwtService = jwtService;

        public async Task<string?> AuthenticateAsync(string name, string Password, string role)
        {
            var bytes = System.Text.Encoding.ASCII.GetBytes(Password);
            var user = await _repository.GetUserByCredentialsAsync(name, bytes, role);

            if (user == null)
                return null;
             
            return _jwtService.GenerateToken(user.IdUser, user.Name, user.RoleId);
        }

        public async Task<string?> SendRecoveryCodeAsync(SendRecoveryCodeRequestDto request)
        {
            var user = (int)await _userRepository.GetUserByEmailAsync(request.email);
            if (user > 0)
                return null;

            var random = new Random();
            var recoveryCode = random.Next(100000, 999999);

            SetLoginAttemptDBRequestDto newAttempt = new()
            {
                UserId = user,
                EmailEntered = request.email,
                Success = false,
                RecoveryCode = recoveryCode,
                RecoveryCodeUsed = false
            };

            await _repository.SetLoginAttemptAsync(newAttempt);
            //Metodo para generar Codigo de recuperacion de 6 digitos para enviar el correo con el codigo de recuperacion

            return "Codigo de recuperación generado con exito";
        }

        public async Task<string?> GenerateNewPasswordAsync(GenerateNewPasswordRequestDto request)
        {
            var response = await _repository.GenerateNewPasswordAsync(new());

            return "Nueva contraseña generada con exito";
        }
    }
}
