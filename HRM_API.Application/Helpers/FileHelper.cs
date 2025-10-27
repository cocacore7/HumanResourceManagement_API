using HRM_API.Configuration;
using HRM_API.Core.Dtos.General;

namespace HRM_API.Application.Helpers
{
    public class FileHelper(ISettings settings)
    {
        private readonly ISettings _settings = settings;

        public static string FileToBase64(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("La ruta del archivo no puede estar vacía.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"No se encontró el archivo en la ruta especificada: {filePath}");

            // Leer bytes del archivo
            byte[] fileBytes = File.ReadAllBytes(filePath);

            // Convertir a Base64
            string base64String = Convert.ToBase64String(fileBytes);

            // Retornar
            return base64String;
        }

        public string SaveFile(GeneralFormRequestAnswerDto answer, GeneralFormRequestOriginDto origin)
        {
            string filePath =
                Path.Combine(
                origin.Description
                , origin.RegisterId.ToString() ?? string.Empty
                , answer.Code + Path.GetExtension(answer.FileName)?.ToLower()
                 );
            string fullFilePath = Path.Combine(_settings.BasePath, filePath);

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("El filepath no puede ser nulo o vacío", nameof(filePath));

            if (string.IsNullOrWhiteSpace(answer.Base64))
                throw new ArgumentException("El contenido del archivo no puede ser nulo o vacío", nameof(answer.Base64));

            // Obtener nombre de archivo y extensión
            var extension = Path.GetExtension(fullFilePath)?.ToLower();

            // Crear carpeta si no existe
            var directory = Path.GetDirectoryName(fullFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            // Convertir Base64 a bytes
            byte[] fileBytes;
            try
            {
                fileBytes = Convert.FromBase64String(answer.Base64);
            }
            catch (FormatException)
            {
                throw new ArgumentException("El contenido base64 no es válido");
            }

            // Guardar archivo según tipo
            switch (extension)
            {
                case ".pdf":
                    File.WriteAllBytes(fullFilePath, fileBytes);
                    break;

                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".bmp":
                case ".gif":
                    File.WriteAllBytes(fullFilePath, fileBytes);
                    break;

                default:
                    throw new NotSupportedException($"El tipo de archivo '{extension}' no es soportado");
            }
            return filePath;
        }
    }
}
