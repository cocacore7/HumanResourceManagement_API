using HRM_API.Application.Helpers;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Interfaces.Authorization;
using HRM_API.Core.Interfaces.User;
using System.Text;

namespace HRM_API.Application.Services
{
    public class AuthorizationService(IAuthorizationRepository repository, IUserRepository userRepository, JwtService jwtService, AuthorizationHelper authorizationHelper, MailHelper mailHelper)
    {
        private readonly IAuthorizationRepository _repository = repository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly JwtService _jwtService = jwtService;
        private readonly AuthorizationHelper _authorizationHelper = authorizationHelper;
        private readonly MailHelper _mailHelper = mailHelper;

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
            var user = await _userRepository.GetUserByEmailAsync(request.email);
            if (user is null)
                return null;

            var random = new Random();
            var recoveryCode = random.Next(100000, 999999);

            SetLoginAttemptDBRequestDto newAttempt = new()
            {
                UserId = user.IdUser,
                EmailEntered = request.email,
                Success = false,
                RecoveryCode = recoveryCode,
                RecoveryCodeUsed = false
            };

            await _repository.SetLoginAttemptAsync(newAttempt);
            //Metodo para generar Codigo de recuperacion de 6 digitos para enviar el correo con el codigo de recuperacion
            await _mailHelper.SendEmailFromTemplateAsync(request.email ?? string.Empty, "Codigo de validacion de contraseña", "RecoveryCode", 0, user.Name, recoveryCode.ToString());

            return "Codigo de recuperación generado con exito";
        }
            
        public async Task<string?> GenerateNewPasswordAsync(GenerateNewPasswordRequestDto request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            var userId = await _repository.ValidPasswordCodeAsync(request.RecoveryCode, request.Email);
            var isValidCode = await _repository.UpdatePasswordCodeAsync((int)userId);
            if (user == null || userId == null || isValidCode == false)
                return null;

            var plainPassword = _authorizationHelper.GenerateSecurePassword();
            byte[] newPasswordBytes = Encoding.UTF8.GetBytes(plainPassword);

            var response = await _repository.GenerateNewPasswordAsync(new() { UserId = (int)userId, NewPassword = newPasswordBytes });

            //Enviar por correo la nueva contraseña generada (Falta implementar)
            await _mailHelper.SendEmailFromTemplateAsync(request.Email ?? string.Empty, "Nueva Contraseña Generada", "RenewPassword", 0, user.Name, plainPassword);

            return "Nueva contraseña generada con exito";
        }
    }
}
