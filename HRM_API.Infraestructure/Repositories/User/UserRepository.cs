using Dapper;
using HRM_API.Core.Dtos.User;
using HRM_API.Core.Interfaces.User;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.User
{
    public class UserRepository(IConfiguration configuration) : IUserRepository
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<List<GetUserModulesDBResponseDto>?> GetUserModulesAsync(int IdUser)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT m.IdModule, m.Path, m.Icon, m.Label
                        FROM HRM_DB.reclutamiento.Module m
                        INNER JOIN HRM_DB.reclutamiento.UserModule um ON um.ModuleId = m.IdModule
                        WHERE um.UserId = @IdUser";
            var result = await connection.QueryAsync<GetUserModulesDBResponseDto>(sql, new { IdUser });

            return [.. result];
        }

        public async Task<GetUserByEmailDBResponseDto?> GetUserByEmailAsync(string Email)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT u.IdUser, u.Name
                        FROM HRM_DB.reclutamiento.Users u
                        WHERE u.Email = @Email
                        AND u.IsActive = 1";
            var result = await connection.QueryFirstOrDefaultAsync<GetUserByEmailDBResponseDto?>(sql, new { Email });

            return result;
        }

        public async Task<string?> GetEmailByUserAsync(int IdUser)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT u.Email
                        FROM HRM_DB.reclutamiento.Users u
                        WHERE u.IdUser = @IdUser
                        AND u.IsActive = 1";
            var result = await connection.QueryFirstOrDefaultAsync<string?>(sql, new { IdUser });

            return result;
        }

        public async Task<string?> GetEmailByUserAsync(int IdUser)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT u.Email
                        FROM HRM_DB.reclutamiento.Users u
                        WHERE u.IdUser = @IdUser";
            var result = await connection.QueryFirstOrDefaultAsync<string?>(sql, new { IdUser });

            return result;
        }
    }
}
