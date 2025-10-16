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
    }
}
