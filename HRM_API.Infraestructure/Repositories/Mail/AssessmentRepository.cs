using Dapper;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Interfaces.Mail;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.Mail
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly IConfiguration _configuration;

        public AssessmentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<AssessmentTestDto?> GetAssessmentTestAsync(int id)
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

            var user = await connection.QueryFirstOrDefaultAsync<AssessmentTestDto>(sql, new {id});

            return user;
        }
    }
}