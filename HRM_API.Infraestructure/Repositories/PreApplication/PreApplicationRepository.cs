using Dapper;
using HRM_API.Core.Dtos.PreApplication;
using HRM_API.Core.Interfaces.PreApplication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.PreApplication
{
    public class PreApplicationRepository : IPreApplicationRepository
    {
        private readonly IConfiguration _configuration;

        public PreApplicationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<GetPreApplicationsDto>?> GetPreApplicationsAsync(string estado)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.IdPreApplication AS [id], pa.FullName AS [nombre], pa.DPI AS [dpi], pa.Age AS [edad], 
                        pa.Gender AS [genero], pa.Phone AS [telefono], pa.Email AS [correo],t.TownName AS [departamento], 
                        pa.Address as [direccion], jv.JobPositionName AS [puesto], pa.Status AS [estado], 
                        pa.assignedTo AS [assignedTo], pa.EducationLevel AS [ultimoGrado], pa.HowHeard AS [fuente], 
                        pa.AssignHub AS [hub], pa.IsReferred AS [esReferido], pa.RefferedBy AS [referidoPor]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv ON jv.IdVacancy = pa.VacancyId
                        LEFT JOIN HRM_DB.reclutamiento.Town t ON t.IdTown = pa.TownId
                        WHERE pa.Status = @estado
                        ORDER BY pa.CreatedAt DESC";
            var result = await connection.QueryAsync<GetPreApplicationsDto>(sql, new { estado });

            return result.ToList();
        }
    }
}
