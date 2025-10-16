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
                        WHERE IdForm = @FormId";
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

            var sql = @"SELECT paa.IdAnswer, paa.QuestionId, fq.Code, paa.AnswerType, paa.ValueBool, paa.ValueText, 
                        paa.ValueNumber, paa.ValueDate, f.IdFile, f.FileName, f.ContentType, f.FilePath, f.SizeBytes,
                        fqo.IdOption, fqo.Value, fqo.Label
                        FROM HRM_DB.reclutamiento.PreApplicationAnswer paa
                        INNER JOIN HRM_DB.reclutamiento.FormQuestion fq ON fq.IdQuestion = paa.QuestionId
                        LEFT JOIN HRM_DB.reclutamiento.PreApplicationAnswerOption paao ON paao.AnswerId = paa.IdAnswer
                        LEFT JOIN HRM_DB.reclutamiento.FormQuestionOption fqo ON fqo.IdOption = paao.OptionId
                        LEFT JOIN HRM_DB.reclutamiento.Files f ON f.IdFile = paa.FileId
                        WHERE ResponseId = @IdResponse";
            var result = await connection.QueryAsync<GetFormAnswersDBAnswersResponseDto>(sql, new { IdResponse });

            return [.. result];
        }
    }
}
