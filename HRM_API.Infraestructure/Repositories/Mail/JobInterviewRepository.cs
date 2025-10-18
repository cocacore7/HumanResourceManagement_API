using Dapper;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Interfaces.Mail;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.Mail
{
    public class JobInterviewRepository : IMailRepository
    {
        private readonly IConfiguration _configuration;

        public JobInterviewRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<JobInterviewDto?> GetJobInterviewAsync(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.IdPreApplication [Code]
                              ,pa.FullName
                              ,pa.Age
                              ,pa.Gender
                              ,pa.Phone
                              ,pa.Email
                              ,jb.Action
                              ,pa.CreatedAt
                              ,pah.Note
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jb
                        ON jb.IdVacancy  = pa.VacancyId 
                        INNER JOIN HRM_DB.reclutamiento.PreApplicationHistory  pah
                        ON pah.PreApplicationId    = pa.IdPreApplication
                        WHERE pa.IdPreApplication = @id";

            var jobinterview = await connection.QueryFirstOrDefaultAsync<JobInterviewDto>(sql, new {id});

            if (jobinterview  == null)
                throw new Exception("No se encontraron datos para el correo.");

            return jobinterview;
        }
    }
}