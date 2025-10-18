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
                            pa.AssignTo AS [assignedTo], pa.EducationLevel AS [ultimoGrado], pa.HowHeard AS [fuente], 
                            pa.AssignHub AS [hub], pa.IsReferred AS [esReferido], pa.RefferedBy AS [referidoPor]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.JobVacancy jv ON jv.IdVacancy = pa.VacancyId
                            LEFT JOIN HRM_DB.reclutamiento.Town t ON t.IdTown = pa.TownId
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
                            pa.AssignTo AS [assignedTo], pa.EducationLevel AS [ultimoGrado], pa.HowHeard AS [fuente], 
                            pa.AssignHub AS [hub], pa.IsReferred AS [esReferido], pa.RefferedBy AS [referidoPor]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.JobVacancy jv ON jv.IdVacancy = pa.VacancyId
                            LEFT JOIN HRM_DB.reclutamiento.Town t ON t.IdTown = pa.TownId
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

        public async Task<bool?> SetPreApplicationsAsync(SetPreApplicationsDBRequestDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.PreApplication 
                        (FullName, DPI, Age, Gender, Phone, Email, TownId, Address, EducationLevel, VacancyId, 
                        Experience, HowHeard, CVFileId, AcceptedTerms, Origin, Status, IsReferred, RefferedBy, CreatedAt, CreatedBy) 
                        VALUES 
                        (@FullName, @DPI, @Age, @Gender, @Phone, @Email, @TownId, @Address, @EducationLevel,@VacancyId, 
                        @Experience, @HowHeard, @CVFileId, @AcceptedTerms, @Origin, @Status, @IsReferred, @RefferedBy, @CreatedAt, @CreatedBy);";

            var rowsAffected = await connection.ExecuteAsync(sql, request);

            return true;
        }
    }
}
