using Dapper;
using HRM_API.Core.Dtos.Mail;
using HRM_API.Core.Interfaces.Mail;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.Mail
{
    public class MailRepository : IMailRepository
    {
        private readonly IConfiguration _configuration;

        public MailRepository(IConfiguration configuration)
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
                              ,c.Comment           [Comment]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv
                        ON jv.IdVacancy  = pa.VacancyId
                        INNER JOIN HRM_DB.reclutamiento.Users u
                        ON pa.AssignTo =  u.IdUser
                        OUTER APPLY (
                            SELECT STRING_AGG(cm.CommentText, ', ')    [Comment]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.Comment cm
                            ON cm.PreApplicationId    = pa.IdPreApplication
                            WHERE pa.IdPreApplication = @id
                            AND CommentStatus = 'entrevista'
                        ) c
                        WHERE pa.IdPreApplication = @id";

            var assessment = await connection.QueryFirstOrDefaultAsync<AssessmentTestDto>(sql, new {id});

            if (assessment == null)
                throw new Exception("No se encontraron datos para el correo.");

            return assessment;
        }

        public async Task<BossInterviewDto?> GetBossInterviewAsync(int id)
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
                              ,c.Comment           [Comment]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv
                        ON jv.IdVacancy  = pa.VacancyId
                        INNER JOIN HRM_DB.reclutamiento.Users u
                        ON pa.AssignTo =  u.IdUser
                        OUTER APPLY (
                            SELECT STRING_AGG(cm.CommentText, ', ')    [Comment]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.Comment cm
                            ON cm.PreApplicationId    = pa.IdPreApplication
                            WHERE pa.IdPreApplication = @id
                            AND CommentStatus = 'entrevistaJefe'
                        ) c
                        WHERE pa.IdPreApplication = @id";

            var boosinterview = await connection.QueryFirstOrDefaultAsync<BossInterviewDto>(sql, new {id});

            if (boosinterview  == null)
                throw new Exception("No se encontraron datos para el correo.");

            return boosinterview;
        }

        public async Task<CandidateRecordDto?> GetCandidateRecordAsync(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.IdPreApplication [Code]
                              ,pa.FullName         [FullName]
                              ,jv.JobPositionName  [JobPositionName]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv
                        ON jv.IdVacancy  = pa.VacancyId
                        WHERE pa.IdPreApplication = @id";

            var candidateRecord = await connection.QueryFirstOrDefaultAsync<CandidateRecordDto>(sql, new {id});

            if (candidateRecord  == null)
                throw new Exception("No se encontraron datos para el correo.");

            return candidateRecord;
        }

        public async Task<EndProcessDto?> GetEndProcessAsync(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.IdPreApplication [Code]
                              ,pa.FullName         [FullName]
                              ,jv.JobPositionName  [JobPositionName]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv
                        ON jv.IdVacancy  = pa.VacancyId
                        WHERE pa.IdPreApplication = @id";

            var endprocess = await connection.QueryFirstOrDefaultAsync<EndProcessDto>(sql, new {id});

            if (endprocess  == null)
                throw new Exception("No se encontraron datos para el correo.");

            return endprocess;
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
                              ,c.Comment           [Comment]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv
                        ON jv.IdVacancy  = pa.VacancyId
                        INNER JOIN HRM_DB.reclutamiento.Users u
                        ON pa.AssignTo =  u.IdUser
                        OUTER APPLY (
                            SELECT STRING_AGG(cm.CommentText, ', ')    [Comment]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.Comment cm
                            ON cm.PreApplicationId    = pa.IdPreApplication
                            WHERE pa.IdPreApplication = @id
                            AND CommentStatus = 'entrevista'
                        ) c
                        WHERE pa.IdPreApplication = @id";

            var jobinterview = await connection.QueryFirstOrDefaultAsync<JobInterviewDto>(sql, new {id});

            if (jobinterview  == null)
                throw new Exception("No se encontraron datos para el correo.");

            return jobinterview;
        }

        public async Task<PolygraphDto?> GetPolygraphAsync(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.IdPreApplication [Code]
                              ,u.Name               [FullName_Recruiter] 
                              ,pa.FullName          [FullName]
                              ,pa.Age               [Age]
                              ,pa.Gender            [Gender]
                              ,pa.Phone             [Phone]
                              ,pa.Email             [Email]
                              ,jv.JobPositionName   [JobPositionName]
                              ,pa.CreatedAt         [CreatedAt]
                              ,c.Comment            [Comment]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv
                        ON jv.IdVacancy  = pa.VacancyId
                        INNER JOIN HRM_DB.reclutamiento.Users u
                        ON pa.AssignTo =  u.IdUser
                        OUTER APPLY (
                            SELECT STRING_AGG(cm.CommentText, ', ')    [Comment]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.Comment cm
                            ON cm.PreApplicationId    = pa.IdPreApplication
                            WHERE pa.IdPreApplication = @id
                            AND CommentStatus = 'poligrafo'
                        ) c
                        WHERE pa.IdPreApplication = @id";

            var polygraph= await connection.QueryFirstOrDefaultAsync<PolygraphDto>(sql, new {id});

            if (polygraph  == null)
                throw new Exception("No se encontraron datos para el correo.");

            return polygraph;
        }

        public async Task<PreScreeningDto?> GetPreScreeningAsync(int id)
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
                              ,c.Comment           [Comment]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv
                        ON jv.IdVacancy  = pa.VacancyId
                        INNER JOIN HRM_DB.reclutamiento.Users u
                        ON pa.AssignTo =  u.IdUser
                        OUTER APPLY (
                            SELECT STRING_AGG(cm.CommentText, ', ')    [Comment]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.Comment cm
                            ON cm.PreApplicationId    = pa.IdPreApplication
                            WHERE pa.IdPreApplication = @id
                            AND CommentStatus = 'Prefiltro'
                        ) c
                        WHERE pa.IdPreApplication = @id";

            var preScreening= await connection.QueryFirstOrDefaultAsync<PreScreeningDto>(sql, new {id});

            if (preScreening  == null)
                throw new Exception("No se encontraron datos para el correo.");

            return preScreening;
        }

        public async Task<RecordDto?> GetRecordAsync(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.IdPreApplication [Code]
                              ,u.Name              [FullName_Recruiter] 
                              ,pa.FullName         [FullName]
                              ,pa.DPI              [DPI]
                              ,jv.JobPositionName  [JobPositionName]    
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv
                        ON jv.IdVacancy  = pa.VacancyId 
                        INNER JOIN HRM_DB.reclutamiento.Users u
                        ON pa.AssignTo =  u.IdUser
                        WHERE pa.IdPreApplication = @id";

            var record= await connection.QueryFirstOrDefaultAsync<RecordDto>(sql, new {id});

            if (record  == null)
                throw new Exception("No se encontraron datos para el correo.");

            return record;
        }
    }
}