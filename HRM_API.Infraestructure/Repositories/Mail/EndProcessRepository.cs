using Dapper;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Interfaces.Mail;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.Mail
{
    public class EndProcessRepository : IMailRepository
    {
        private readonly IConfiguration _configuration;

        public EndProcessRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<EndProcessDto?> GetEndProcessAsync(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.IdPreApplication [Code]
                              ,pa.FullName         [FullName]
                              ,jv.JobPositionName  [JobPositionName]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv
                        ON jb.IdVacancy  = pa.VacancyId
                        WHERE pa.IdPreApplication = @id";

            var endprocess = await connection.QueryFirstOrDefaultAsync<EndProcessDto>(sql, new {id});

            if (endprocess  == null)
                throw new Exception("No se encontraron datos para el correo.");

            return endprocess;
        }
    }
}