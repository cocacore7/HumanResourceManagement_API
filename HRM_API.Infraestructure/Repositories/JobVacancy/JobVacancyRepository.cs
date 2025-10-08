using Dapper;
using HRM_API.Core.Dtos.JobVacancy;
using HRM_API.Core.Interfaces.JobVacancy;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.JobVacancy
{
    public class JobVacancyRepository : IJobVacancyRepository
    {
        private readonly IConfiguration _configuration;

        public JobVacancyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<GetJobVacanciesDto>?> GetJobVacanciesAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

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
                        ORDER BY jv.CreatedAt DESC";
            var result = await connection.QueryAsync<GetJobVacanciesDto>(sql, new { });

            return result.ToList();
        }
    }
}
