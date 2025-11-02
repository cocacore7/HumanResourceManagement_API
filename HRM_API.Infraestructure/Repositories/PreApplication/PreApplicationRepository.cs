using Dapper;
using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Dtos.PreApplication;
using HRM_API.Core.Interfaces.PreApplication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.PreApplication
{
    public class PreApplicationRepository(IConfiguration configuration) : IPreApplicationRepository
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<List<GetPreApplicationsDBResponseDto>?> GetPreApplicationsAsync(string estado, string id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            if (!string.IsNullOrEmpty(estado))
            {
                var sql = @"SELECT pa.IdPreApplication AS [id], pa.FullName AS [nombre], pa.DPI AS [dpi], pa.Age AS [edad], 
                            pa.Gender AS [genero], pa.Phone AS [telefono], pa.Email AS [correo],t.TownName AS [departamento], 
                            pa.Address as [direccion], jv.JobPositionName AS [puesto], pa.Status AS [estado], 
                            u.Name AS [assignedTo], pa.EducationLevel AS [ultimoGrado], pa.HowHeard AS [fuente], 
                            pa.AssignHub AS [hub], pa.IsReferred AS [esReferido], pa.RefferedBy AS [referidoPor]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.JobVacancy jv ON jv.IdVacancy = pa.VacancyId
                            LEFT JOIN HRM_DB.reclutamiento.Town t ON t.IdTown = pa.TownId
                            LEFT JOIN HRM_DB.reclutamiento.Users u ON u.IdUser = pa.AssignTo
                            WHERE pa.Status = @estado
                            ORDER BY pa.CreatedAt DESC";
                var result = await connection.QueryAsync<GetPreApplicationsDBResponseDto>(sql, new { estado });

                return [.. result];
            }
            else if (!string.IsNullOrEmpty(id))
            {
                var sql = @"SELECT pa.IdPreApplication AS [id], pa.FullName AS [nombre], pa.DPI AS [dpi], pa.Age AS [edad], 
                            pa.Gender AS [genero], pa.Phone AS [telefono], pa.Email AS [correo],t.TownName AS [departamento], 
                            pa.Address as [direccion], jv.JobPositionName AS [puesto], pa.Status AS [estado], 
                            u.Name AS [assignedTo], pa.EducationLevel AS [ultimoGrado], pa.HowHeard AS [fuente], 
                            pa.AssignHub AS [hub], pa.IsReferred AS [esReferido], pa.RefferedBy AS [referidoPor]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.JobVacancy jv ON jv.IdVacancy = pa.VacancyId
                            LEFT JOIN HRM_DB.reclutamiento.Town t ON t.IdTown = pa.TownId
                            LEFT JOIN HRM_DB.reclutamiento.Users u ON u.IdUser = pa.AssignTo
                            WHERE pa.IdPreApplication = @id
                            ORDER BY pa.CreatedAt DESC";
                var result = await connection.QueryAsync<GetPreApplicationsDBResponseDto>(sql, new { id });

                return [.. result];
            }
            else
            {
                return [];
            }
        }

        public async Task<List<GetPreApplicationsDBResponseDto>?> GetPreApplicationsFailedAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.IdPreApplication AS [id], pa.FullName AS [nombre], pa.DPI AS [dpi], pa.Age AS [edad], 
                        pa.Gender AS [genero], pa.Phone AS [telefono], pa.Email AS [correo],t.TownName AS [departamento], 
                        pa.Address as [direccion], jv.JobPositionName AS [puesto], pa.Status AS [estado], 
                        u.Name AS [assignedTo], pa.EducationLevel AS [ultimoGrado], pa.HowHeard AS [fuente], 
                        pa.AssignHub AS [hub], pa.IsReferred AS [esReferido], pa.RefferedBy AS [referidoPor]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv ON jv.IdVacancy = pa.VacancyId
                        LEFT JOIN HRM_DB.reclutamiento.Town t ON t.IdTown = pa.TownId
                        LEFT JOIN HRM_DB.reclutamiento.Users u ON u.IdUser = pa.AssignTo
                        WHERE pa.Status IN ('rechazoPreFiltro','rechazoSolicitud','rechazoRevision','rechazoDescartado','rechazoEntrevista',
                                            'rechazoPruebas','rechazoEntrevistaJefe','rechazoPoligrafo','rechazoCargaExpediente',
                                            'rechazoContratacion','rechazoDescartado')
                        ORDER BY pa.CreatedAt DESC";
            var result = await connection.QueryAsync<GetPreApplicationsDBResponseDto>(sql, new { });

            return [.. result];
        }

        public async Task<List<GetPreApplicationsDBResponseDto>?> GetPreApplicationsProcessAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.IdPreApplication AS [id], pa.FullName AS [nombre], pa.DPI AS [dpi], pa.Age AS [edad], 
                        pa.Gender AS [genero], pa.Phone AS [telefono], pa.Email AS [correo],t.TownName AS [departamento], 
                        pa.Address as [direccion], jv.JobPositionName AS [puesto], pa.Status AS [estado], 
                        u.Name AS [assignedTo], pa.EducationLevel AS [ultimoGrado], pa.HowHeard AS [fuente], 
                        pa.AssignHub AS [hub], pa.IsReferred AS [esReferido], pa.RefferedBy AS [referidoPor]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv ON jv.IdVacancy = pa.VacancyId
                        LEFT JOIN HRM_DB.reclutamiento.Town t ON t.IdTown = pa.TownId
                        LEFT JOIN HRM_DB.reclutamiento.Users u ON u.IdUser = pa.AssignTo
                        WHERE pa.Status IN ('preFiltro','solicitud','entrevista','pruebas','entrevistaJefe',
                                            'poligrafo','cargaExpedienteNoCargado','cargaExpedienteParcial',
                                            'cargaExpedienteCompletado','contratacion')
                        ORDER BY pa.CreatedAt DESC";
            var result = await connection.QueryAsync<GetPreApplicationsDBResponseDto>(sql, new { });

            return [.. result];
        }

        public async Task<List<GetPreApplicationsDBResponseDto>?> GetPreApplicationsByUserAsync(string estado, string id, int userId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            if (!string.IsNullOrEmpty(estado))
            {
                var sql = @"SELECT pa.IdPreApplication AS [id], pa.FullName AS [nombre], pa.DPI AS [dpi], pa.Age AS [edad], 
                            pa.Gender AS [genero], pa.Phone AS [telefono], pa.Email AS [correo],t.TownName AS [departamento], 
                            pa.Address as [direccion], jv.JobPositionName AS [puesto], pa.Status AS [estado], 
                            u.Name AS [assignedTo], pa.EducationLevel AS [ultimoGrado], pa.HowHeard AS [fuente], 
                            pa.AssignHub AS [hub], pa.IsReferred AS [esReferido], pa.RefferedBy AS [referidoPor]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.JobVacancy jv ON jv.IdVacancy = pa.VacancyId
                            LEFT JOIN HRM_DB.reclutamiento.Town t ON t.IdTown = pa.TownId
                            LEFT JOIN HRM_DB.reclutamiento.Users u ON u.IdUser = pa.AssignTo
                            WHERE pa.Status = @estado
                            AND pa.AssignTo = @userId
                            ORDER BY pa.CreatedAt DESC";
                var result = await connection.QueryAsync<GetPreApplicationsDBResponseDto>(sql, new { estado, userId });

                return [.. result];
            }
            else if (!string.IsNullOrEmpty(id))
            {
                var sql = @"SELECT pa.IdPreApplication AS [id], pa.FullName AS [nombre], pa.DPI AS [dpi], pa.Age AS [edad], 
                            pa.Gender AS [genero], pa.Phone AS [telefono], pa.Email AS [correo],t.TownName AS [departamento], 
                            pa.Address as [direccion], jv.JobPositionName AS [puesto], pa.Status AS [estado], 
                            u.Name AS [assignedTo], pa.EducationLevel AS [ultimoGrado], pa.HowHeard AS [fuente], 
                            pa.AssignHub AS [hub], pa.IsReferred AS [esReferido], pa.RefferedBy AS [referidoPor]
                            FROM HRM_DB.reclutamiento.PreApplication pa
                            INNER JOIN HRM_DB.reclutamiento.JobVacancy jv ON jv.IdVacancy = pa.VacancyId
                            LEFT JOIN HRM_DB.reclutamiento.Town t ON t.IdTown = pa.TownId
                            LEFT JOIN HRM_DB.reclutamiento.Users u ON u.IdUser = pa.AssignTo
                            WHERE pa.IdPreApplication = @id
                            AND pa.AssignTo = @userId
                            ORDER BY pa.CreatedAt DESC";
                var result = await connection.QueryAsync<GetPreApplicationsDBResponseDto>(sql, new { id , userId });

                return [.. result];
            }
            else
            {
                return [];
            }
        }

        public async Task<List<GetFormAnswersDBAnswersResponseDto>?> GetPreApplicationFormsFilesAsync(int? id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"WITH CTE AS (
                            SELECT 
                                paa.IdAnswer,
                                paa.QuestionId,
                                fq.Code,
                                paa.AnswerType,
                                paa.ValueBool,
                                paa.ValueText,
                                paa.ValueNumber,
                                paa.ValueDate,
                                f.IdFile,
                                f.FileName,
                                f.ContentType,
                                f.FilePath,
                                f.SizeBytes,
                                fqo.IdOption,
                                fqo.Value,
                                fqo.Label,
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
                            IdAnswer,
                            QuestionId,
                            Code,
                            AnswerType,
                            ValueBool,
                            ValueText,
                            ValueNumber,
                            ValueDate,
                            IdFile,
                            FileName,
                            ContentType,
                            FilePath,
                            SizeBytes,
                            IdOption,
                            Value,
                            Label,
							PreApplicationId
                        FROM CTE
                        WHERE PreApplicationId =  @id;";
            var result = await connection.QueryAsync<GetFormAnswersDBAnswersResponseDto?>(sql, new { id });

            return [.. result];
        }

        public async Task<GetCountPreApplicationByStateDBResponseDto?> GetCountPreApplicationByStateAsync(string state)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.Status [State], COUNT(pa.Status) [Count]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        WHERE pa.Status = @state
                        GROUP BY pa.Status";
            var result = await connection.QueryFirstOrDefaultAsync<GetCountPreApplicationByStateDBResponseDto>(sql, new { state });

            return (GetCountPreApplicationByStateDBResponseDto?)result;
        }

        public async Task<GetCountPreApplicationByStateDBResponseDto?> GetHiredCountPreApplicationAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.Status [State], COUNT(pa.Status) [Count]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        WHERE pa.Status = 'contratado'
                        GROUP BY pa.Status";
            var result = await connection.QueryFirstOrDefaultAsync<GetCountPreApplicationByStateDBResponseDto>(sql, new { });

            return (GetCountPreApplicationByStateDBResponseDto?)result;
        }

        public async Task<GetCountPreApplicationByStateDBResponseDto?> GetFailCountPreApplicationAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT 'failedStatus' [State], COUNT(pa.Status) [Count]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        WHERE pa.Status IN ('rechazoPreFiltro','rechazoSolicitud','rechazoRevision','rechazoDescartado','rechazoEntrevista',
                                            'rechazoPruebas','rechazoEntrevistaJefe','rechazoPoligrafo','rechazoCargaExpediente',
                                            'rechazoContratacion','rechazoDescartado')";
            var result = await connection.QueryFirstOrDefaultAsync<GetCountPreApplicationByStateDBResponseDto>(sql, new {  });

            return (GetCountPreApplicationByStateDBResponseDto?)result;
        }

        public async Task<GetCountPreApplicationByStateDBResponseDto?> GetProcessCountPreApplicationAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT 'prcessStatus' [State], COUNT(pa.Status) [Count]
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        WHERE pa.Status IN ('preFiltro','solicitud','entrevista','pruebas','entrevistaJefe',
                                            'poligrafo','cargaExpedienteNoCargado','cargaExpedienteParcial',
                                            'cargaExpedienteCompletado','contratacion')";
            var result = await connection.QueryFirstOrDefaultAsync<GetCountPreApplicationByStateDBResponseDto>(sql, new {  });

            return (GetCountPreApplicationByStateDBResponseDto?)result;
        }

        public async Task<List<GetFormAnswersDBAnswersResponseDto>?> GetPreApplicationPreApplicationFileAsync(int? id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT 
                            0 AS IdAnswer,
                            0 AS QuestionId,
                            'ADJUNTA_CV' AS Code,
                            'file' AS AnswerType,
                            NULL AS ValueBool,
                            NULL AS ValueText,
                            NULL AS ValueNumber,
                            NULL AS ValueDate,
                            f.IdFile,
                            f.FileName,
                            f.ContentType,
                            f.FilePath,
                            f.SizeBytes,
                            0 AS IdOption,
                            0 AS Value,
                            0 AS Label
                        FROM reclutamiento.PreApplication pa
                        INNER JOIN reclutamiento.Files f ON f.IdFile = pa.CVFileId
                        WHERE pa.IdPreApplication = @id;";
            var result = await connection.QueryAsync<GetFormAnswersDBAnswersResponseDto?>(sql, new { id });

            return [.. result];
        }

        public async Task<int?> GetPreApplicationFileIdAsync(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT f.IdFile
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.Files f ON f.IdFile = pa.CVFileId
                        WHERE pa.IdPreApplication = @id";
            var result = await connection.QueryFirstAsync<int?>(sql, new { id });

            return result;
        }

        public async Task<bool?> PreAppValidatePublicAsync(long? dpi, int? id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"SELECT pa.IdPreApplication
                        FROM HRM_DB.reclutamiento.PreApplication pa
                        INNER JOIN HRM_DB.reclutamiento.JobVacancy jv ON jv.IdVacancy = pa.VacancyId
                        WHERE pa.IdPreApplication = @id
                        AND pa.DPI = @dpi
                        AND jv.Status = 'nuevaVacante'
                        AND pa.Status = 'cargaExpedienteNoCargado'";
            var result = await connection.QueryFirstOrDefaultAsync<int?>(sql, new { id, dpi });

            return result > 0;
        }

        public async Task<int?> SetPreApplicationsAsync(SetPreApplicationsDBRequestDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.PreApplication 
                        (FullName, DPI, Age, Gender, Phone, Email, TownId, Address, EducationLevel, VacancyId, 
                        Experience, HowHeard, CVFileId, AcceptedTerms, Origin, Status, IsReferred, RefferedBy, CreatedAt, CreatedBy) 
                        VALUES 
                        (@FullName, @DPI, @Age, @Gender, @Phone, @Email, @TownId, @Address, @EducationLevel,@VacancyId, 
                        @Experience, @HowHeard, @CVFileId, @AcceptedTerms, @Origin, @Status, @IsReferred, @RefferedBy, @CreatedAt, @CreatedBy);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            var rowsAffected = await connection.QueryFirstAsync<int>(sql, new
            {
                request.FullName,
                request.DPI,
                request.Age,
                request.Gender,
                request.Phone,
                request.Email,
                request.TownId,
                request.Address,
                request.EducationLevel,
                request.VacancyId,
                request.Experience,
                request.HowHeard,
                request.CVFileId,
                request.AcceptedTerms,
                request.Origin,
                request.Status,
                request.IsReferred,
                request.RefferedBy,
                request.CreatedAt,
                request.CreatedBy
            });

            return rowsAffected;
        }

        public async Task<bool?> UpdatePreApplicationsAsync(UpdatePreApplicationsDBRequestDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            if (request.Experience == null)
            {

                var sql = @"UPDATE HRM_DB.reclutamiento.PreApplication 
                        SET FullName = @FullName, DPI = @DPI, Age = @Age, Gender = @Gender, Phone = @Phone, Email = @Email, 
                        TownId = @TownId, Address = @Address, EducationLevel = @EducationLevel, VacancyId = @VacancyId, 
                        HowHeard = @HowHeard, AssignHub = @AssignHub, IsReferred = @IsReferred, RefferedBy = @RefferedBy
                        WHERE IdPreApplication = @IdPreApplication";

                var rowsAffected = await connection.ExecuteAsync(sql, new {
                    request.FullName,
                    request.DPI,
                    request.Age,
                    request.Gender,
                    request.Phone,
                    request.Email,
                    request.TownId,
                    request.Address,
                    request.EducationLevel,
                    request.VacancyId,
                    request.HowHeard,
                    request.AssignHub,
                    request.IsReferred,
                    request.RefferedBy,
                    request.IdPreApplication
                });

                return rowsAffected > 0;
            }
            else
            {

                var sql = @"UPDATE HRM_DB.reclutamiento.PreApplication 
                        SET FullName = @FullName, DPI = @DPI, Age = @Age, Gender = @Gender, Phone = @Phone, Email = @Email, 
                        TownId = @TownId, Address = @Address, EducationLevel = @EducationLevel, VacancyId = @VacancyId, 
                        Experience = @Experience, HowHeard = @HowHeard, AcceptedTerms = @AcceptedTerms, Origin = @Origin, 
                        Status = @Status, IsReferred = @IsReferred, RefferedBy = @RefferedBy
                        WHERE IdPreApplication = @IdPreApplication";

                var rowsAffected = await connection.ExecuteAsync(sql, new
                {
                    request.FullName,
                    request.DPI,
                    request.Age,
                    request.Gender,
                    request.Phone,
                    request.Email,
                    request.TownId,
                    request.Address,
                    request.EducationLevel,
                    request.VacancyId,
                    request.Experience,
                    request.HowHeard,
                    request.AcceptedTerms,
                    request.Origin,
                    request.Status,
                    request.IsReferred,
                    request.RefferedBy,
                    request.IdPreApplication
                });

                return rowsAffected > 0;
            }
        }

        public async Task<bool?> UpdateIsDocumentedAsync(int? PreApplicationId, bool? IsDocumented)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplication 
                        SET IsDocumentedByCandidate = @IsDocumented
                        WHERE IdPreApplication = @PreApplicationId;";
            var result = await connection.ExecuteAsync(sql, new { PreApplicationId, IsDocumented });

            return result > 0;
        }

        public async Task<bool?> UpdateStatusAssignToAsync(int? PreApplicationId, string? Status, int? AssignTo)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplication 
                        SET Status = @Status, AssignTo = @AssignTo
                        WHERE  IdPreApplication = @PreApplicationId;";
            var result = await connection.ExecuteAsync(sql, new { Status, AssignTo, PreApplicationId });

            return result > 0;
        }

        public async Task<bool?> UpdateStatusFailAsync(int? PreApplicationId, string? Status)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplication 
                        SET Status = @Status
                        WHERE  IdPreApplication = @PreApplicationId;";
            var result = await connection.ExecuteAsync(sql, new { Status, PreApplicationId });

            return result > 0;
        }

        public async Task<bool?> UpdateVacancyCountAsync(int? PreApplicationId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"DECLARE @idVacante INT,
                                @nPlazas INT;
                            SELECT @idVacante = VacancyId
                            FROM HRM_DB.reclutamiento.PreApplication 
                            WHERE  IdPreApplication = @PreApplicationId;

                            SELECT @nPlazas = AvailablePositions 
                              FROM HRM_DB.reclutamiento.JobVacancy
                              WHERE idVacancy = @idVacante

                             IF(@nPlazas > 1)
                             BEGIN 
	                             UPDATE HRM_DB.reclutamiento.JobVacancy
	                               SET AvailablePositions = (CAST(AvailablePositions AS INT) - 1)
                                 WHERE idVacancy = @idVacante
                             END
                             ELSE IF (@nPlazas = 1)
                             BEGIN 
	                             UPDATE HRM_DB.reclutamiento.JobVacancy
	                               SET AvailablePositions = (CAST(AvailablePositions AS INT) - 1)
                                 WHERE idVacancy = @idVacante
	 
	                             UPDATE HRM_DB.reclutamiento.JobVacancy
	                               SET [Status] = 'cubiertoNuevaVacante'
                                 WHERE idVacancy = @idVacante
                             END";
            var result = await connection.ExecuteAsync(sql, new { PreApplicationId });

            return result > 0;
        }


        public async Task<bool?> AssignToAsync(int? PreapplicationId, int? IdUserAssign)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"UPDATE HRM_DB.reclutamiento.PreApplication 
                        SET AssignTo = @IdUserAssign
                        WHERE  IdPreApplication = @PreapplicationId;";
            var result = await connection.ExecuteAsync(sql, new { IdUserAssign, PreapplicationId });

            return result > 0;
        }
    }
}
