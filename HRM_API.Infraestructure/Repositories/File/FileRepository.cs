using Dapper;
using HRM_API.Core.Dtos.File;
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

            if (folderName == "Preapplication")
            {
                var sql = @"SELECT f.FilePath
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.PreApplicationFormResponse pf ON pf.PreApplicationId = pa.IdPreApplication
                            INNER JOIN HRM_DB.reclutamiento.PreApplicationAnswer paa ON paa.ResponseId = pf.IdResponse
                            INNER JOIN HRM_DB.reclutamiento.Files f ON f.IdFile = paa.FileId
                            INNER JOIN HRM_DB.reclutamiento.FormQuestion fq ON fq.IdQuestion = paa.QuestionId
                            WHERE pa.IdPreApplication = @id
                            AND fq.Code = @code";
                var result = await connection.QueryFirstAsync<GetFileBase64DBResponseDto>(sql, new { id, code });

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
