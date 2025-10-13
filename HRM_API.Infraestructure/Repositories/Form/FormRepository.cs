using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Interfaces.Form;

namespace HRM_API.Infraestructure.Repositories.Form
{
    public class FormRepository(IConfiguration configuration) : IFormRepository
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<GetFormAnswersDBFormResponseDto?> GetFormAsync(int FormId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT IdForm, KeyName, Name, VersionNumber
                        FROM HRM_DB.reclutamiento.Form
                        WHERE f.IdForm = @FormId";
            var result = await connection.QueryFirstAsync<GetFormAnswersDBFormResponseDto>(sql, new { FormId });

            return result;
        }

        public async Task<GetFormAnswersDBHeaderResponseDto?> GetFormHeaderAsync(int PreApplicationId, int FormId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pafr.IdResponse, pafr.CreatedAt, pafr.CreatedAt, pafr.UpdatedAt
                        FROM HRM_DB.reclutamiento.PreApplicationFormResponse pafr
                        WHERE pafr.PreApplicationId = @PreApplicationId
                        AND pafr.FormId = @FormId";
            var result = await connection.QueryFirstAsync<GetFormAnswersDBHeaderResponseDto>(sql, new { PreApplicationId, FormId });

            return result;
        }

        public async Task<List<GetFormAnswersDBAnswersResponseDto>?> GetFormAnswersAsync(int? IdResponse)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT m.IdModule, m.Path, m.Icon, m.Label
                        FROM HRM_DB.reclutamiento.Module m
                        INNER JOIN HRM_DB.reclutamiento.UserModule um ON um.ModuleId = m.IdModule
                        WHERE um.UserId = @IdUser";
            var result = await connection.QueryAsync<GetFormAnswersDBAnswersResponseDto>(sql, new { IdResponse });

            return [.. result];
        }
    }
}
