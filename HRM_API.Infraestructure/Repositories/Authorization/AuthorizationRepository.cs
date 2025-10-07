using Dapper;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Interfaces.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.Authorization
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly IConfiguration _configuration;

        public AuthorizationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<UserDto?> GetUserByCredentialsAsync(string name, byte[] Password, string role)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT u.IdUser, u.Name, u.RoleId 
                        FROM HRM_DB.reclutamiento.Users u
                        INNER JOIN HRM_DB.reclutamiento.Role r ON r.IdRole = u.RoleId
                        WHERE u.Name = @Name 
                        AND u.PasswordHash = @Password 
                        AND r.KeyName = @Role";
            var user = await connection.QueryFirstOrDefaultAsync<UserDto>(sql, new {name, Password, role });

            //var parameters = new DynamicParameters();
            //parameters.Add("@IdUser", idUser, DbType.String);
            //parameters.Add("@Name", name, DbType.String);
            //parameters.Add("@Role", role, DbType.String);

            //var user = await connection.QueryFirstOrDefaultAsync<UserModel>(
            //    "[dbo].[sp_ValidateUserCredentials]",
            //    parameters,
            //    commandType: CommandType.StoredProcedure
            //);

            return user;
        }
    }
}
