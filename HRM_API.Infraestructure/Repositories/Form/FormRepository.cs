using Dapper;
using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Interfaces.Form;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.Form
{
    public class FormRepository(IConfiguration configuration) : IFormRepository
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
