using Dapper;
using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Interfaces.Form;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.Form
{
    public class FormRepository : IFormRepository
    {
        private readonly IConfiguration _configuration;

        public FormRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<FormDto?> GetUserModulesAsync(string userId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT u.IdUser, u.Name, u.RoleId 
                        FROM HRM_DB.reclutamiento.Users u
                        INNER JOIN HRM_DB.reclutamiento.Role r ON r.IdRole = u.RoleId
                        WHERE u.Name = @Name 
                        AND u.PasswordHash = @Password 
                        AND r.KeyName = @Role";
            var user = await connection.QueryFirstOrDefaultAsync<FormDto>(sql, new { userId });

            return user;
        }
    }
}
