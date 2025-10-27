using Azure.Core;
using Dapper;
using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Interfaces.Form;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
            var result = await connection.QueryFirstAsync<GetFormAnswersDBFormResponseDto?>(sql, new { FormId });

            return result;
        }

        public async Task<GetFormAnswersDBHeaderResponseDto?> GetFormHeaderAsync(int PreApplicationId, int FormId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pafr.IdResponse, pafr.CreatedAt, pafr.CreatedAt, pafr.UpdatedAt
                        FROM HRM_DB.reclutamiento.PreApplicationFormResponse pafr
                        WHERE pafr.PreApplicationId = @PreApplicationId
                        AND pafr.FormId = @FormId";
            var result = await connection.QueryFirstAsync<GetFormAnswersDBHeaderResponseDto?>(sql, new { PreApplicationId, FormId });

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
            var result = await connection.QueryAsync<GetFormAnswersDBAnswersResponseDto?>(sql, new { IdResponse });

            return [.. result];
        }

        public async Task<GetPreApplicationFormResponseDBResponseDto?> GetPreApplicationFormResponseAsync(int? PreApplicationId, int? FormId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pafr.IdResponse
                        FROM HRM_DB.reclutamiento.PreApplicationFormResponse pafr
                        WHERE pafr.PreApplicationId = @PreApplicationId
                        AND pafr.FormId = @FormId";
            var result = await connection.QuerySingleOrDefaultAsync<GetPreApplicationFormResponseDBResponseDto?>(sql, new { PreApplicationId, FormId });

            return result;
        }

        public async Task<GetValidQuestionDBResponseDto?> GetValidQuestionAsync(int? FormId, string Code)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT fq.IdQuestion AS QuestionId,fq.Type
                        FROM HRM_DB.reclutamiento.FormQuestion fq
                        WHERE fq.FormId = @FormId
                        AND fq.Code = @Code";
            var result = await connection.QueryFirstAsync<GetValidQuestionDBResponseDto>(sql, new { FormId, Code });

            return result;
        }

        public async Task<int?> GetQuestionOptionAsync(int? QuestionId, int? CatalogId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT fqo.IdOption
                        FROM HRM_DB.reclutamiento.FormQuestionOption fqo
                        WHERE fqo.QuestionId = @QuestionId
                        AND fqo.CatalogId = @CatalogId";
            var result = await connection.QuerySingleOrDefaultAsync<int?>(sql, new { QuestionId, CatalogId });

            return result;
        }

        public async Task<int?> GetPreApplicationAnswerFileIdAsync(int? QuestionId, int? ResponseId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT paa.FileId
                        FROM HRM_DB.reclutamiento.PreApplicationAnswer paa
                        WHERE paa.ResponseId = @ResponseId
                        AND paa.QuestionId = @QuestionId";
            var result = await connection.QueryFirstAsync<int>(sql, new { ResponseId, QuestionId });

            return result;
        }

        public async Task<int?> SetPreApplicationFormResponseAsync(SetPreApplicationFormResponseDBRequestDto PreApplicationForm)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.PreApplicationFormResponse 
                        (PreApplicationId, FormId, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
                        VALUES
                        (@PreApplicationId,@FormId,@CreatedAt,@CreatedBy,@UpdatedAt,@UpdatedBy);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
            var result = await connection.QueryFirstAsync<int>(sql, PreApplicationForm);

            return result;
        }

        public async Task<bool?> SetTextAnswerAsync(SetTextAnswerDBResponseDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.PreApplicationAnswer 
                        (ResponseId, QuestionId, AnswerType, ValueText, CreatedAt, UpdatedAt)
                        VALUES
                        (@ResponseId,@QuestionId,@AnswerType,@ValueText,@CreatedAt,@UpdatedAt);";
            var result = await connection.ExecuteAsync(sql, request);

            return result > 0;
        }

        public async Task<bool?> SetNumberAnswerAsync(SetNumberAnswerDBResponseDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.PreApplicationAnswer 
                        (ResponseId, QuestionId, AnswerType, ValueNumber, CreatedAt, UpdatedAt)
                        VALUES
                        (@ResponseId,@QuestionId,@AnswerType,@ValueNumber,@CreatedAt,@UpdatedAt);";
            var result = await connection.ExecuteAsync(sql, request);

            return result > 0;
        }

        public async Task<bool?> SetBoolAnswerAsync(SetBoolAnswerDBResponseDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.PreApplicationAnswer 
                        (ResponseId, QuestionId, AnswerType, ValueBool, CreatedAt, UpdatedAt)
                        VALUES
                        (@ResponseId,@QuestionId,@AnswerType,@ValueBool,@CreatedAt,@UpdatedAt);";
            var result = await connection.ExecuteAsync(sql, request);

            return result > 0;
        }

        public async Task<bool?> SetFileAnswerAsync(SetFileAnswerDBResponseDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.PreApplicationAnswer 
                        (ResponseId, QuestionId, AnswerType, FileId, CreatedAt, UpdatedAt)
                        VALUES
                        (@ResponseId,@QuestionId,@AnswerType,@FileId,@CreatedAt,@UpdatedAt);";
            var result = await connection.ExecuteAsync(sql, request);

            return result > 0;
        }

        public async Task<bool?> SetDateAnswerAsync(SetDateAnswerDBResponseDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.PreApplicationAnswer 
                        (ResponseId, QuestionId, AnswerType, ValueDate, CreatedAt, UpdatedAt)
                        VALUES
                        (@ResponseId,@QuestionId,@AnswerType,@ValueDate,@CreatedAt,@UpdatedAt);";
            var result = await connection.ExecuteAsync(sql, request);

            return result > 0;
        }

        public async Task<int?> SetEnumAnswerAsync(SetEnumAnswerDBResponseDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.PreApplicationAnswer 
                        (ResponseId, QuestionId, AnswerType, CreatedAt, UpdatedAt)
                        VALUES
                        (@ResponseId,@QuestionId,@AnswerType,@CreatedAt,@UpdatedAt);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
            var result = await connection.QueryFirstAsync<int>(sql, request);

            return result;
        }

        public async Task<bool?> SetEnumAnswerOptionAsync(int? AnswerId, int? OptionId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.PreApplicationAnswerOption 
                        (AnswerId, OptionId)
                        VALUES
                        (@AnswerId,@OptionId);";
            var result = await connection.ExecuteAsync(sql, new { AnswerId, OptionId });

            return result > 0;
        }

        public async Task<bool?> UpdateTextAnswerAsync(UpdateTextAnswerDBResponseDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplicationAnswer 
                        SET AnswerType = @AnswerType, ValueText = @ValueText, UpdatedAt = @UpdatedAt
                        WHERE ResponseId = @ResponseId
                        AND QuestionId = @QuestionId;";
            var result = await connection.ExecuteAsync(sql, new { request });

            return result > 0;
        }

        public async Task<bool?> UpdateNumberAnswerAsync(UpdateNumberAnswerDBResponseDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplicationAnswer 
                        SET AnswerType = @AnswerType, ValueNumber = @ValueNumber, UpdatedAt = @UpdatedAt
                        WHERE ResponseId = @ResponseId
                        AND QuestionId = @QuestionId;";
            var result = await connection.ExecuteAsync(sql, new { request });

            return result > 0;
        }

        public async Task<bool?> UpdateBoolAnswerAsync(UpdateBoolAnswerDBResponseDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplicationAnswer
                        SET AnswerType = @AnswerType, ValueBool = @ValueBool, UpdatedAt = @UpdatedAt
                        WHERE ResponseId = @ResponseId
                        AND QuestionId = @QuestionId;";
            var result = await connection.ExecuteAsync(sql, new { request });

            return result > 0;
        }

        public async Task<bool?> UpdateFileAnswerAsync(UpdateFileAnswerDBResponseDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplicationAnswer
                        SET AnswerType = @AnswerType, UpdatedAt = @UpdatedAt
                        WHERE ResponseId = @ResponseId
                        AND QuestionId = @QuestionId;";
            var result = await connection.ExecuteAsync(sql, new { request });

            return result > 0;
        }

        public async Task<bool?> UpdateDateAnswerAsync(UpdateDateAnswerDBResponseDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplicationAnswer
                        SET AnswerType = @AnswerType, ValueDate = @ValueDate, UpdatedAt = @UpdatedAt
                        WHERE ResponseId = @ResponseId
                        AND QuestionId = @QuestionId;";
            var result = await connection.ExecuteAsync(sql, new { request });

            return result > 0;
        }

        public async Task<int?> UpdateEnumAnswerAsync(UpdateEnumAnswerDBResponseDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplicationAnswer
                        SET AnswerType = @AnswerType, UpdatedAt = @UpdatedAt
                        WHERE ResponseId = @ResponseId
                        AND QuestionId = @QuestionId;";
            var result = await connection.QueryFirstAsync<int>(sql, new { request });

            return result;
        }

        public async Task<bool?> UpdateEnumAnswerOptionAsync(int? AnswerId, int? OptionId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplicationAnswerOption 
                        SET OptionId = @OptionId
                        WHERE AnswerId = @AnswerId;";
            var result = await connection.ExecuteAsync(sql, new { AnswerId, OptionId });

            return result > 0;
        }
    }
}
