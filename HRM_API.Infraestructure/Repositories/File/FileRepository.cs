using Dapper;
using HRM_API.Core.Dtos.File;
using HRM_API.Core.Interfaces.File;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.File
{
    public class FileRepository : IFileRepository
    {
        private readonly IConfiguration _configuration;

        public FileRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GetFileBase64Dto?> GetFileBase64Async(string folderName, int id, string code)
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
                var result = await connection.QueryFirstAsync<GetFileBase64Dto>(sql, new { id, code });

                return result;
            }
            else if (folderName == "Vacation")
            {
                var sql = @"SELECT f.FilePath
                            FROM HRM_DB.reclutamiento.JobVacancy jv
                            INNER JOIN HRM_DB.reclutamiento.Files f ON f.IdFile = jv.RequisitionFileId
                            WHERE jv.IdVacancy = @id";
                var result = await connection.QueryFirstAsync<GetFileBase64Dto>(sql, new { id });

                return result;
            }
            else 
            {
                return new GetFileBase64Dto();
            }
        }
    }
}
