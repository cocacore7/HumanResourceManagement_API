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
                        AND r.KeyName = @Role
                        AND u.IsActive = 1";
            var user = await connection.QueryFirstOrDefaultAsync<LoginDBResponseDto>(sql, new {name, Password, role });

            return user;
        }

        public async Task<int?> SetLoginAttemptAsync(SetLoginAttemptDBRequestDto requestdb)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.LoginAttempt
                        (UserId, EmailEntered, Success, RecoveryCode, RecoveryCodeUsed, CreatedAt)
                        VALUES
                        (@UserId,@EmailEntered,@Success,@RecoveryCode,@RecoveryCodeUsed,@CreatedAt);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
            var user = await connection.QueryFirstOrDefaultAsync<int?>(sql, requestdb);

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
                        AND r.KeyName = @Role
                        AND u.IsActive = 1";
            var user = await connection.QueryFirstOrDefaultAsync<GenerateNewPasswordDBResponseDto>(sql, new { requestdb });

            return user;
        }

        public async Task<int?> ValidPasswordCodeAsync(int code, string email)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT la.UserId
                        FROM HRM_DB.reclutamiento.LoginAttempt la
                        WHERE la.EmailEntered = @email
                        AND la.RecoveryCode = @code";
            var user = await connection.QueryFirstOrDefaultAsync<int>(sql, new { code, email });

            return user;
        }

        public async Task<bool?> UpdatePasswordCodeAsync(int userId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.LoginAttempt
                        SET RecoveryCodeUsed = 1
                        WHERE UserId = @UserId
                        AND RecoveryCodeUsed = 0";
            var user = await connection.QueryFirstOrDefaultAsync<int>(sql, new { userId });

            return user > 0;
        }
    }
}
