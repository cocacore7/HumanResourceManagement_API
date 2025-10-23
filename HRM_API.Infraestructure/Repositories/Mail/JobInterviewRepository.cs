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
                        AND CommentStatus = 'entrevista'";

            var jobinterview = await connection.QueryFirstOrDefaultAsync<JobInterviewDto>(sql, new {id});

            if (jobinterview  == null)
                throw new Exception("No se encontraron datos para el correo.");

            return jobinterview;
        }
    }
}