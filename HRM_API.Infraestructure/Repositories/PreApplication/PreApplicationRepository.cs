using Dapper;
using HRM_API.Core.Dtos.PreApplication;
using HRM_API.Core.Interfaces.PreApplication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.PreApplication
{
    public class PreApplicationRepository(IConfiguration configuration) : IPreApplicationRepository
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<List<GetPreApplicationsDBResponseDto>?> GetPreApplicationsAsync(string estado, string id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            if (!string.IsNullOrEmpty(estado))
            {
                var sql = @"SELECT pa.IdPreApplication AS [id], pa.FullName AS [nombre], pa.DPI AS [dpi], pa.Age AS [edad], 
                            pa.Gender AS [genero], pa.Phone AS [telefono], pa.Email AS [correo],t.TownName AS [departamento], 
                            pa.Address as [direccion], jv.JobPositionName AS [puesto], pa.Status AS [estado], 
                            u.Name AS [assignedTo], pa.EducationLevel AS [ultimoGrado], pa.HowHeard AS [fuente], 
                            pa.AssignHub AS [hub], pa.IsReferred AS [esReferido], pa.RefferedBy AS [referidoPor]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.JobVacancy jv ON jv.IdVacancy = pa.VacancyId
                            LEFT JOIN HRM_DB.reclutamiento.Town t ON t.IdTown = pa.TownId
                            LEFT JOIN HRM_DB.reclutamiento.Users u ON u.IdUser = pa.AssignTo
                            WHERE pa.Status = @estado
                            ORDER BY pa.CreatedAt DESC";
                var result = await connection.QueryAsync<GetPreApplicationsDBResponseDto>(sql, new { estado });

                return [.. result];
            }
            else if (!string.IsNullOrEmpty(id))
            {
                var sql = @"SELECT pa.IdPreApplication AS [id], pa.FullName AS [nombre], pa.DPI AS [dpi], pa.Age AS [edad], 
                            pa.Gender AS [genero], pa.Phone AS [telefono], pa.Email AS [correo],t.TownName AS [departamento], 
                            pa.Address as [direccion], jv.JobPositionName AS [puesto], pa.Status AS [estado], 
                            u.Name AS [assignedTo], pa.EducationLevel AS [ultimoGrado], pa.HowHeard AS [fuente], 
                            pa.AssignHub AS [hub], pa.IsReferred AS [esReferido], pa.RefferedBy AS [referidoPor]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.JobVacancy jv ON jv.IdVacancy = pa.VacancyId
                            LEFT JOIN HRM_DB.reclutamiento.Town t ON t.IdTown = pa.TownId
                            LEFT JOIN HRM_DB.reclutamiento.Users u ON u.IdUser = pa.AssignTo
                            WHERE pa.IdPreApplication = @id
                            ORDER BY pa.CreatedAt DESC";
                var result = await connection.QueryAsync<GetPreApplicationsDBResponseDto>(sql, new { id });

                return [.. result];
            }
            else
            {
                return [];
            }
        }

        public async Task<int?> GetPreApplicationFileIdAsync(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT f.IdFile
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.Files f ON f.IdFile = pa.CVFileId
                        WHERE pa.IdPreApplication = @id";
            var result = await connection.QueryFirstAsync<int?>(sql, new { id });

            return result;
        }

        public async Task<int?> SetPreApplicationsAsync(SetPreApplicationsDBRequestDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.PreApplication 
                        (FullName, DPI, Age, Gender, Phone, Email, TownId, Address, EducationLevel, VacancyId, 
                        Experience, HowHeard, CVFileId, AcceptedTerms, Origin, Status, IsReferred, RefferedBy, CreatedAt, CreatedBy) 
                        VALUES 
                        (@FullName, @DPI, @Age, @Gender, @Phone, @Email, @TownId, @Address, @EducationLevel,@VacancyId, 
                        @Experience, @HowHeard, @CVFileId, @AcceptedTerms, @Origin, @Status, @IsReferred, @RefferedBy, @CreatedAt, @CreatedBy);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            var rowsAffected = await connection.QueryFirstAsync<int>(sql, request);

            return rowsAffected;
        }

        public async Task<bool?> UpdatePreApplicationsAsync(UpdatePreApplicationsDBRequestDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplication 
                        SET FullName = @FullName, DPI = @DPI, Age = @Age, Gender = @Gender, Phone = @Phone, Email = @Email, 
                        TownId = @TownId, Address = @Address, EducationLevel = @EducationLevel, VacancyId = @VacancyId, 
                        Experience = @Experience, HowHeard = @HowHeard, AcceptedTerms = @AcceptedTerms, Origin = @Origin, 
                        Status = @Status, IsReferred = @IsReferred, RefferedBy = @RefferedBy
                        WHERE IdPreApplication = @IdPreApplication";

            var rowsAffected = await connection.ExecuteAsync(sql, new { request.FullName, request.DPI, request.Age, request.Gender, request.Phone, request.Email,
                request.TownId, request.Address, request.EducationLevel, request.VacancyId, request.Experience, request.HowHeard, request.AcceptedTerms,
                request.Origin, request.Status, request.IsReferred, request.RefferedBy, request.IdPreApplication});

            return true;
        }

        public async Task<bool?> UpdateIsDocumentedAsync(int? PreApplicationId, bool? IsDocumented)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplication 
                        SET IsDocumentedByCandidate = @IsDocumented
                        WHERE IdPreApplication = @PreApplicationId;";
            var result = await connection.ExecuteAsync(sql, new { PreApplicationId, IsDocumented });

            return result > 0;
        }

        public async Task<bool?> UpdateStatusAssignToAsync(int? PreApplicationId, string? Status, int? AssignTo)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplication 
                        SET Status = @Status, AssignTo = @AssignTo
                        WHERE  IdPreApplication = @PreApplicationId;";
            var result = await connection.ExecuteAsync(sql, new { Status, AssignTo, PreApplicationId });

            return result > 0;
        }
    }
}
