using Dapper;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Interfaces.Catalog;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.Catalog
{
    public class CatalogRepository(IConfiguration configuration) : ICatalogRepository
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<List<CatalogDBRequestDto>> GetJobCatalogAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT jv.IdVacancy AS Id, jv.JobPositionName AS Value, jv.JobPositionName AS Label
                        FROM HRM_DB.reclutamiento.JobVacancy jv";
            var result = await connection.QueryAsync<CatalogDBRequestDto?>(sql, new { });

            return [.. result];
        }

        public async Task<List<CatalogDBRequestDto>> GetTownCatalogAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT t.IdTown AS Id, t.TownName AS Value, t.TownName AS Label
                        FROM HRM_DB.reclutamiento.Town t";
            var result = await connection.QueryAsync<CatalogDBRequestDto?>(sql, new { });

            return [.. result];
        }

        public async Task<List<CatalogDBRequestDto>> GetTermsAndConditionsCatalogAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT lt.IdTerm AS Id, '' AS Value, lt.Content AS Label
                        FROM HRM_DB.reclutamiento.LegalTerm lt";
            var result = await connection.QueryAsync<CatalogDBRequestDto?>(sql, new { });

            return [.. result];
        }

        public async Task<List<CatalogDBRequestDto>> GetVacancyReasonCatalogAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT vr.IdReason AS Id, vr.VacancyReasonName AS Value, vr.VacancyReasonName AS Label
                        FROM HRM_DB.reclutamiento.VacancyReason vr";
            var result = await connection.QueryAsync<CatalogDBRequestDto?>(sql, new { });

            return [.. result];
        }

        public async Task<List<CatalogDBRequestDto>> GetVacancyTypeCatalogAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT vt.IdVacancyType AS Id, vt.VacancyTypeName AS Value, vt.VacancyTypeName AS Label
                        FROM HRM_DB.reclutamiento.VacancyType vt";
            var result = await connection.QueryAsync<CatalogDBRequestDto?>(sql, new { });

            return [.. result];
        }
    }
}