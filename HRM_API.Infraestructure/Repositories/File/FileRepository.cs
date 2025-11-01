using Dapper;
using HRM_API.Core.Dtos.File;
using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Interfaces.File;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.File
{
    public class FileRepository(IConfiguration configuration) : IFileRepository
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<GetFileBase64DBResponseDto?> GetFileBase64Async(string folderName, int id, string code)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            if (folderName == "PreApplication")
            {
                string sql = "";
                if (code.Contains("ADJUNTA_CV"))
                {
                    sql = @"DECLARE @isCode BIT;

                            ;WITH CTE AS
                            (
                                SELECT 
                                    1 AS isCode,
                                    pafr.PreApplicationId,
                                    ROW_NUMBER() OVER (PARTITION BY fq.Code ORDER BY paa.IdAnswer DESC) AS rn
                                FROM HRM_DB.reclutamiento.PreApplicationFormResponse pafr
                                INNER JOIN HRM_DB.reclutamiento.PreApplicationAnswer paa 
                                    ON paa.ResponseId = pafr.IdResponse
                                INNER JOIN HRM_DB.reclutamiento.FormQuestion fq 
                                    ON fq.IdQuestion = paa.QuestionId
                                LEFT JOIN HRM_DB.reclutamiento.PreApplicationAnswerOption paao 
                                    ON paao.AnswerId = paa.IdAnswer
                                LEFT JOIN HRM_DB.reclutamiento.FormQuestionOption fqo 
                                    ON fqo.IdOption = paao.OptionId
                                LEFT JOIN HRM_DB.reclutamiento.Files f 
                                    ON f.IdFile = paa.FileId
                                WHERE paa.AnswerType = 'file'
                                  AND (f.FileName IS NOT NULL OR f.FileName <> '')
                                  AND f.IdFile IS NOT NULL
                                  AND fq.Code = 'ADJUNTA_CV'
                            )
                            SELECT TOP 1 @isCode = isCode
                            FROM CTE
                            WHERE PreApplicationId = @id;

                            IF (@isCode = 1)
                            BEGIN
                                ;WITH CTE AS
                                (
                                    SELECT 
                                        f.FilePath,
                                        pafr.PreApplicationId,
                                        ROW_NUMBER() OVER (PARTITION BY fq.Code ORDER BY paa.IdAnswer DESC) AS rn
                                    FROM HRM_DB.reclutamiento.PreApplicationFormResponse pafr
                                    INNER JOIN HRM_DB.reclutamiento.PreApplicationAnswer paa 
                                        ON paa.ResponseId = pafr.IdResponse
                                    INNER JOIN HRM_DB.reclutamiento.FormQuestion fq 
                                        ON fq.IdQuestion = paa.QuestionId
                                    LEFT JOIN HRM_DB.reclutamiento.PreApplicationAnswerOption paao 
                                        ON paao.AnswerId = paa.IdAnswer
                                    LEFT JOIN HRM_DB.reclutamiento.FormQuestionOption fqo 
                                        ON fqo.IdOption = paao.OptionId
                                    LEFT JOIN HRM_DB.reclutamiento.Files f 
                                        ON f.IdFile = paa.FileId
                                    WHERE paa.AnswerType = 'file'
                                      AND (f.FileName IS NOT NULL OR f.FileName <> '')
                                      AND f.IdFile IS NOT NULL
                                )
                                SELECT FilePath
                                FROM CTE
                                WHERE PreApplicationId = @id;
                            END
                            ELSE
                            BEGIN
                                SELECT 
                                    f.FilePath
                                FROM HRM_DB.reclutamiento.PreApplication pa
                                INNER JOIN HRM_DB.reclutamiento.Files f 
                                    ON f.IdFile = pa.CVFileId
                                WHERE pa.IdPreApplication = @id;
                            END;
                            ";
                }
                else
                {
                    sql = @"WITH CTE AS (
                            SELECT f.FilePath,
								pafr.PreApplicationId,
                                ROW_NUMBER() OVER (PARTITION BY fq.Code ORDER BY paa.IdAnswer DESC) AS rn
                            FROM HRM_DB.reclutamiento.PreApplicationFormResponse pafr
                            INNER JOIN HRM_DB.reclutamiento.PreApplicationAnswer paa 
                                ON paa.ResponseId = pafr.IdResponse
                            INNER JOIN HRM_DB.reclutamiento.FormQuestion fq 
                                ON fq.IdQuestion = paa.QuestionId
                            LEFT JOIN HRM_DB.reclutamiento.PreApplicationAnswerOption paao 
                                ON paao.AnswerId = paa.IdAnswer
                            LEFT JOIN HRM_DB.reclutamiento.FormQuestionOption fqo 
                                ON fqo.IdOption = paao.OptionId
                            LEFT JOIN HRM_DB.reclutamiento.Files f 
                                ON f.IdFile = paa.FileId
                            WHERE paa.AnswerType = 'file'
                            AND (f.FileName <> NULL OR f.FileName <> '')
                            AND f.IdFile IS NOT NULL
                        )
                        SELECT 
                            FilePath
                        FROM CTE
                        WHERE PreApplicationId =  @id;";
                }
                var result = await connection.QueryFirstOrDefaultAsync<GetFileBase64DBResponseDto>(sql, new { code, id });

                return result;
            }
            else if (folderName == "Vacancy")
            {
                var sql = @"SELECT f.FilePath
                            FROM HRM_DB.reclutamiento.JobVacancy jv
                            INNER JOIN HRM_DB.reclutamiento.Files f ON f.IdFile = jv.RequisitionFileId
                            WHERE jv.IdVacancy = @id";
                var result = await connection.QueryFirstAsync<GetFileBase64DBResponseDto>(sql, new { id });

                return result;
            }
            else 
            {
                return new GetFileBase64DBResponseDto();
            }
        }

        public async Task<int?> SetFileAsync(SetFileDBRequestDto File)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.Files 
                        (FileName, ContentType, FilePath, SizeBytes, UploadedBy, UploadedAt) 
                        VALUES 
                        (@FileName, @ContentType, @FilePath, @SizeBytes, @UploadedBy, @UploadedAt);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
            var result = await connection.QueryFirstOrDefaultAsync<int?>(sql, File);

            return result;
        }

        public async Task<bool?> UpdateFileAsync(UpdateFileDBRequestDto File)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.Files 
                        SET FileName = @FileName, ContentType = @ContentType, FilePath = @FilePath,
                        SizeBytes = @SizeBytes, UploadedAt = @UploadedAt 
                        WHERE IdFile = @IdFile";
            var result = await connection.ExecuteAsync(sql, new { File.FileName, File.ContentType, File.FilePath, File.SizeBytes, File.UploadedAt, File.IdFile });

            return result > 0;
        }
    }
}
