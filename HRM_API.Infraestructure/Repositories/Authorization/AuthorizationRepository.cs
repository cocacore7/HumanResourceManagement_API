using Dapper;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Interfaces.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.Authorization
{
    public class AuthorizationRepository(IConfiguration configuration) : IAuthorizationRepository
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<LoginDBResponseDto?> GetUserByCredentialsAsync(string name, byte[] Password, string role)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT u.IdUser, u.Name, u.RoleId 
                        FROM HRM_DB.reclutamiento.Users u
                        INNER JOIN HRM_DB.reclutamiento.Role r ON r.IdRole = u.RoleId
                        WHERE u.Name = @Name 
                        AND u.PasswordHash = @Password 
                        AND r.KeyName = @Role";
            var user = await connection.QueryFirstOrDefaultAsync<LoginDBResponseDto>(sql, new {name, Password, role });

            return user;
        }

        public async Task<SetLoginAttemptDBResponseDto?> SetLoginAttemptAsync(SetLoginAttemptDBRequestDto requestdb)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT u.IdUser, u.Name, u.RoleId 
                        FROM HRM_DB.reclutamiento.Users u
                        INNER JOIN HRM_DB.reclutamiento.Role r ON r.IdRole = u.RoleId
                        WHERE u.Name = @Name 
                        AND u.PasswordHash = @Password 
                        AND r.KeyName = @Role";
            var user = await connection.QueryFirstOrDefaultAsync<SetLoginAttemptDBResponseDto>(sql, new { requestdb });

            return user;
        }

        public async Task<GenerateNewPasswordDBResponseDto?> GenerateNewPasswordAsync(GenerateNewPasswordDBRequestDto requestdb)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT u.IdUser, u.Name, u.RoleId 
                        FROM HRM_DB.reclutamiento.Users u
                        INNER JOIN HRM_DB.reclutamiento.Role r ON r.IdRole = u.RoleId
                        WHERE u.Name = @Name 
                        AND u.PasswordHash = @Password 
                        AND r.KeyName = @Role";
            var user = await connection.QueryFirstOrDefaultAsync<GenerateNewPasswordDBResponseDto>(sql, new { requestdb });

            return user;
        }
    }
}
