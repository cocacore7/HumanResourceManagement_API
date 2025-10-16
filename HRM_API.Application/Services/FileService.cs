using HRM_API.Application.Helpers;
using HRM_API.Configuration;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.File;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Interfaces.File;

namespace HRM_API.Application.Services
{
    public class FileService(IFileRepository repository, FileHelper fileHelper, ISettings settings)
    {
        private readonly IFileRepository _repository = repository;
        private readonly FileHelper _fileHelper = fileHelper;
        private readonly ISettings _settings = settings;

        public async Task<GetFileBase64ResponseDto?> GetFileBase64Async(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("La ruta del archivo no puede estar vacía.", nameof(filePath));

            var normalizedPath = filePath.Replace("\\", "/");
            var decodedPath = Uri.UnescapeDataString(normalizedPath);
            var segments = decodedPath.Trim('/').Split('/');

            if (segments.Length < 3)
                throw new ArgumentException($"La ruta '{filePath}' no tiene el formato esperado '/Tipo/Id/Codigo'.");

            string folderName = segments[0];  // Ejemplo: "Vacancy"
            string idString = segments[1];    // Ejemplo: "5"
            string code = segments[2];        // Ejemplo: "REQUISICION.pdf"
            var questionCode = code.Trim('.').Split('.');

            if (!int.TryParse(idString, out int id))
                throw new ArgumentException($"El valor '{idString}' no es un ID válido.");

            var filePathDB = await _repository.GetFileBase64Async(folderName, id, questionCode[0]);

            if (filePathDB == null || string.IsNullOrEmpty(filePathDB.FilePath))
                return new GetFileBase64ResponseDto
                {
                    Response = new GetFileBase64DBResponseDto { FilePath = "" }
                };

            string basePath = _settings.BasePath;

            if (string.IsNullOrWhiteSpace(basePath))
                throw new InvalidOperationException("No se encontró 'FileSettings:BasePath' en configuración.");

            string fullPath = Path.Combine(basePath, filePathDB.FilePath);

            string fileBase64 = _fileHelper.FileToBase64(fullPath);

            var response = new GetFileBase64ResponseDto
            {
                Response = new GetFileBase64DBResponseDto { FilePath = fileBase64 }
            };

            return response;
        }

        public async Task<SetFileResponseDto?> SetFileAsync(GeneralFormRequestDto request, LoginDBResponseDto user)
        {
            SetFileResponseDto response = new();

            foreach (var item in request.Answers ?? Enumerable.Empty<GeneralFormRequestAnswerDto>())
            {
                var filepath = _fileHelper.SaveFile(item, request?.Origin ?? new GeneralFormRequestOriginDto());

                SetFileDBRequestDto newfile = new()
                {
                    FileName = item.FileName ?? string.Empty,
                    ContentType = item.ContentType ?? string.Empty,
                    FilePath = filepath ?? string.Empty,
                    SizeBytes = item.SizeBytes ?? 0,
                    UploadedBy = int.TryParse(user.IdUser, out int createdBy) ? createdBy : 0
                };

                var responsedb = await _repository.SetFileAsync(newfile);
                response.Response.Add(responsedb == 1 ? "Archivo Guardado Exitosamente" : "Error Archivo, No Se Pudo Guardar");
            }

            return response;
        }
    }
}

