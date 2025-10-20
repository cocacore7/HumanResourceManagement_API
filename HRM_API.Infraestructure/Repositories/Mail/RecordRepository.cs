using Dapper;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Interfaces.Mail;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.Mail
{
    public class RecordRepository : IMailRepository
    {
        private readonly IConfiguration _configuration;

        public RecordRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<RecordDto?> GetRecordAsync(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.IdPreApplication [Code]
                              ,pa.FullName
                              ,pa.DPI
                              ,jb.Action
                              ,pa.CreatedAt
                              ,pah.Note
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jb
                        ON jb.IdVacancy  = pa.VacancyId 
                        INNER JOIN HRM_DB.reclutamiento.PreApplicationHistory  pah
                        ON pah.PreApplicationId    = pa.IdPreApplication
                        WHERE pa.IdPreApplication = @id";

            var record= await connection.QueryFirstOrDefaultAsync<RecordDto>(sql, new {id});

            if (record  == null)
                throw new Exception("No se encontraron datos para el correo.");

            return record;
        }
    }
}