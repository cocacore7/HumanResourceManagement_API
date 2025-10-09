using HRM_API.Application.Helpers;
using HRM_API.Configuration;
using HRM_API.Core.Dtos.File;
using HRM_API.Core.Interfaces.File;

namespace HRM_API.Application.Services
{
    public class FileService
    {
        private readonly IFileRepository _repository;
        private readonly FileHelper _fileHelper;
        private readonly ISettings _settings;

        public FileService(IFileRepository repository, FileHelper fileHelper, ISettings settings)
        {
            _repository = repository;
            _fileHelper = fileHelper;
            _settings = settings;
        }

        public async Task<GetFileBase64ResponseDto?> GetFileBase64Async(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("La ruta del archivo no puede estar vacía.", nameof(filePath));

            var decodedPath = Uri.UnescapeDataString(filePath);
            var segments = decodedPath.Trim('/').Split('/');

            if (segments.Length < 3)
                throw new ArgumentException($"La ruta '{filePath}' no tiene el formato esperado '/Tipo/Id/Codigo'.");

            string folderName = segments[0];  // Preapplication o Vacation
            string idString = segments[1];    // id de "Preapplication" o "Vacation"
            string code = segments[2];    // id de "Preapplication" o "Vacation"
            var questionCode = code.Trim('.').Split('.');

            if (!int.TryParse(idString, out int id))
                throw new ArgumentException($"El valor '{idString}' no es un ID válido.");

            var filePathDB = await _repository.GetFileBase64Async(folderName, id, questionCode[0]);

            if (filePathDB == null || string.IsNullOrEmpty(filePathDB.FilePath))
                return new GetFileBase64ResponseDto
                {
                    Response = new GetFileBase64Dto
                    {
                        FilePath = ""
                    }
                };

            string basePath = _settings.BasePath;

            if (string.IsNullOrWhiteSpace(basePath))
                throw new InvalidOperationException("No se encontró 'FileSettings:BasePath' en configuración.");

            string fullPath = Path.Combine(basePath, filePathDB.FilePath);

            string fileBase64 = _fileHelper.FileToBase64(fullPath);

            var response = new GetFileBase64ResponseDto { Response = new GetFileBase64Dto { FilePath = fileBase64 } };

            return response;
        }
    }
}

