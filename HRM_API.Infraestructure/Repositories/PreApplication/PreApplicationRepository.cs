using Dapper;
using HRM_API.Core.Dtos.PreApplication;
using HRM_API.Core.Interfaces.PreApplication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.PreApplication
{
    public class PreApplicationRepository : IPreApplicationRepository
    {
        private readonly IConfiguration _configuration;

        public PreApplicationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<GetPreApplicationsDto>?> GetPreApplicationsAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT m.IdModule, m.Path, m.Icon, m.Label
                        FROM HRM_DB.reclutamiento.Module m
                        INNER JOIN HRM_DB.reclutamiento.UserModule um ON um.ModuleId = m.IdModule
                        WHERE um.UserId = @IdUser";
            var result = await connection.QueryAsync<GetPreApplicationsDto>(sql, new { });

            return result.ToList();
        }
    }
}
