using Dapper;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Interfaces.Mail;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.Mail
{
    public class AssessmentRepository : IMailRepository
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
                              ,u.Name              [FullName_Recruiter] 
                              ,pa.FullName         [FullName]
                              ,pa.Age              [Age]
                              ,pa.Gender           [Gender]
                              ,pa.Phone            [Phone]
                              ,pa.Email            [Email]
                              ,jv.JobPositionName  [JobPositionName]
                              ,pa.CreatedAt        [CreatedAt]
                              ,cm.CommentText      [Comment]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv
                        ON jb.IdVacancy  = pa.VacancyId
                        INNER JOIN HRM_DB.Users u
                        ON pa.AssignTo =  u.IdUser
                        INNER JOIN HRM_DB.reclutamiento.Comment cm
                        ON cm.PreApplicationId    = pa.IdPreApplication
                        WHERE pa.IdPreApplication = @id
                        AND CommentStatus = 'pruebas'";

            var assessment = await connection.QueryFirstOrDefaultAsync<AssessmentTestDto>(sql, new {id});

            if (assessment == null)
                throw new Exception("No se encontraron datos para el correo.");

            return assessment;
        }
    }
}