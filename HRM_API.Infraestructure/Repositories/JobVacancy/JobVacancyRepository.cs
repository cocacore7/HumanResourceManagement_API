using Dapper;
using HRM_API.Core.Dtos.JobVacancy;
using HRM_API.Core.Interfaces.JobVacancy;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.JobVacancy
{
    public class JobVacancyRepository(IConfiguration configuration) : IJobVacancyRepository
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<List<GetJobVacanciesDBRequestDto>?> GetJobVacanciesAsync(string estado, string id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            if (!string.IsNullOrEmpty(estado))
            {
                var sql = @"SELECT jv.IdVacancy AS [id], jv.JobPositionName AS [puesto], jv.RequesterName AS [jefeSolicitante], 
                        jv.RequesterPosition AS [puestoSolicitante], jv.AreaText AS [areaSolicitante], jv.RegionText AS [region], 
                        jv.HubText AS [hubTienda], jv.Objective AS [objetivo], vt.VacancyTypeName AS [tipoPlaza], 
                        vr.VacancyReasonName AS [motivo], jv.Salary AS [salario], jv.AvailablePositions AS [plazasACubrir], 
                        jv.comment AS [comentario], f.FilePath AS [requisicionUrl], jv.CreatedAt AS [fechaPublicacion], 
                        jv.Status AS [estado]
                        FROM HRM_DB.reclutamiento.JobVacancy jv
                        INNER JOIN HRM_DB.reclutamiento.VacancyType vt ON vt.IdVacancyType = jv.VacancyTypeId
                        INNER JOIN HRM_DB.reclutamiento.VacancyReason vr ON vr.IdReason = jv.ReasonId
                        LEFT JOIN HRM_DB.reclutamiento.Files f ON f.IdFile = jv.RequisitionFileId
                        WHERE jv.Status = @estado
                        ORDER BY jv.CreatedAt DESC";
                var result = await connection.QueryAsync<GetJobVacanciesDBRequestDto>(sql, new { estado });

                return [.. result];
            }
            else if (!string.IsNullOrEmpty(id))
            {
                var sql = @"SELECT jv.IdVacancy AS [id], jv.JobPositionName AS [puesto], jv.RequesterName AS [jefeSolicitante], 
                        jv.RequesterPosition AS [puestoSolicitante], jv.AreaText AS [areaSolicitante], jv.RegionText AS [region], 
                        jv.HubText AS [hubTienda], jv.Objective AS [objetivo], vt.VacancyTypeName AS [tipoPlaza], 
                        vr.VacancyReasonName AS [motivo], jv.Salary AS [salario], jv.AvailablePositions AS [plazasACubrir], 
                        jv.comment AS [comentario], f.FilePath AS [requisicionUrl], jv.CreatedAt AS [fechaPublicacion], 
                        jv.Status AS [estado]
                        FROM HRM_DB.reclutamiento.JobVacancy jv
                        INNER JOIN HRM_DB.reclutamiento.VacancyType vt ON vt.IdVacancyType = jv.VacancyTypeId
                        INNER JOIN HRM_DB.reclutamiento.VacancyReason vr ON vr.IdReason = jv.ReasonId
                        LEFT JOIN HRM_DB.reclutamiento.Files f ON f.IdFile = jv.RequisitionFileId
                        WHERE jv.IdVacancy = @id
                        ORDER BY jv.CreatedAt DESC";
                var result = await connection.QueryAsync<GetJobVacanciesDBRequestDto>(sql, new { id });

                return [.. result];
            }
            else
            {
                return [];
            }
        }

        public async Task<int?> GetJobVacancyFileIdAsync(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT jv.RequisitionFileId
                        FROM HRM_DB.reclutamiento.JobVacancy jv
                        WHERE jv.IdVacancy = @id";
            var result = await connection.QueryFirstAsync<int?>(sql, new { id });

            return result;
        }

        public async Task<int?> SetJobVacancyAsync(SetJobVacancyDBRequestDto dbRequest)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.JobVacancy 
                        (JobPositionName, RequesterName, RequesterPosition, AreaText, RegionText, HubText, Objective, Comment, VacancyTypeId, 
                        ReasonId, Salary, TotalPositions, AvailablePositions, RequisitionFileId, Status, CreatedBy, CreatedAt, UpdatedAt) 
                        VALUES 
                        (@JobPositionName, @RequesterName, @RequesterPosition, @AreaText, @RegionText, @HubText, @Objective, @Comment, @VacancyTypeId,
                        @ReasonId, @Salary, @TotalPositions, @AvailablePositions, @RequisitionFileId, @Status, @CreatedBy, @CreatedAt, @UpdatedAt);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            var rowsAffected = await connection.QueryFirstAsync<int>(sql, dbRequest);

            return rowsAffected;
        }

        public async Task<bool?> UpdateJobVacancyAsync(UpdateJobVacancyDBRequestDto dbRequest)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.JobVacancy 
                        SET JobPositionName =@JobPositionName, RequesterName = @RequesterName, RequesterPosition = @RequesterPosition, AreaText = @AreaText,
                        RegionText = @RegionText, HubText = @HubText, Objective = @Objective, Comment = @Comment, VacancyTypeId = @VacancyTypeId, 
                        ReasonId = @ReasonId, Salary = @Salary, TotalPositions = @TotalPositions, AvailablePositions = @AvailablePositions, 
                        Status = @Status, UpdatedAt = @UpdatedAt
                        WHERE IdVacancy = @IdVacancy;";

            var rowsAffected = await connection.ExecuteAsync(sql, dbRequest);

            return rowsAffected > 0;
        }
    }
}
