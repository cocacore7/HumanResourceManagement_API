using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRM_API.Application.Helpers
{
    public class JwtService(IConfiguration configuration)
    {
        private readonly string _secret = configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key");
        private readonly int _expireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"] ?? "60");

        public string GenerateToken(string idUser, string name, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, idUser),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expireMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token, bool ignoreExpiration = false)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = !ignoreExpiration // si es false, no se lanza excepción al expirar
                }, out _);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public string? RefreshToken(string oldToken)
        {
            // Validar el token sin importar si está expirado
            var principal = ValidateToken(oldToken, ignoreExpiration: true);
            if (principal == null)
                return null;

            // Extraer los datos del token anterior
            var idUser = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var name = principal.FindFirst(ClaimTypes.Name)?.Value;
            var role = principal.FindFirst(ClaimTypes.Role)?.Value;

            if (idUser == null || name == null || role == null)
                return null;

            // Generar un nuevo token
            return GenerateToken(idUser, name, role);
        }
    }
}
