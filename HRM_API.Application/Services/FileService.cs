using HRM_API.Application.Helpers;
using HRM_API.Configuration;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.File;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Enum.File;
using HRM_API.Core.Interfaces.File;

namespace HRM_API.Application.Services
{
    public class FileService
    {
        private readonly IFileRepository _repository;
        private readonly FileHelper _fileHelper;
        private readonly EnumHelper _enumHelper;
        private readonly ISettings _settings;

        public FileService(IFileRepository repository, FileHelper fileHelper, EnumHelper enumHelper, ISettings settings)
        {
            _repository = repository;
            _fileHelper = fileHelper;
            _enumHelper = enumHelper;
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
                    Response = new GetFileBase64DBResponseDto
                    {
                        FilePath = ""
                    }
                };

            string basePath = _settings.BasePath;

            if (string.IsNullOrWhiteSpace(basePath))
                throw new InvalidOperationException("No se encontró 'FileSettings:BasePath' en configuración.");

            string fullPath = Path.Combine(basePath, filePathDB.FilePath);

            string fileBase64 = _fileHelper.FileToBase64(fullPath);

            var response = new GetFileBase64ResponseDto { Response = new GetFileBase64DBResponseDto { FilePath = fileBase64 } };

            return response;
        }

        public async Task<SetFileResponseDto?> SetFileAsync(GeneralFormRequestDto request, LoginDBResponseDto user)
        {
            List<SetFileDBRequestDto> requestbd = new List<SetFileDBRequestDto>();
            SetFileResponseDto response = new SetFileResponseDto();

            foreach (var item in request.Answers ?? Enumerable.Empty<GeneralFormRequestAnswerDto>())
            {
                var filepath = _fileHelper.SaveFile(item.Base64 ?? string.Empty, request?.Origin?.description ?? string.Empty, 
                    request?.Origin?.registerId ?? 0, item.Code, item.FileName ?? string.Empty);

                SetFileDBRequestDto newfile = new SetFileDBRequestDto();
                newfile.fileName = item.FileName ?? string.Empty;
                newfile.contentType = item.ContentType ?? string.Empty;
                newfile.filePath = filepath ?? string.Empty;
                newfile.sizeBytes = item.SizeBytes ?? 0;
                newfile.uploadedBy = int.TryParse(user.IdUser, out int createdBy) ? createdBy : 0;

                var responsedb = await _repository.SetFileAsync(newfile);
                response.Response.Add(responsedb == 1 ? "Archivo Guardado Exitosamente" : "Error Archivo, No Se Pudo Guardar");
            }

            return response;
        }
    }
}

