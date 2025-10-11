using HRM_API.Core.Enum.File;

namespace HRM_API.Application.Helpers
{
    public class FileHelper
    {
        private readonly EnumHelper _enumHelper;
        public FileHelper(EnumHelper enumHelper)
        {
            _enumHelper = enumHelper;
        }
        public string FileToBase64(string filePath)
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

        public string SaveFile(string base64File, string description, int registerId, string code, string fileName)
        {
            string filePath =
                Path.Combine(
                description == _enumHelper.GetEnumDescription(SetFileOriginEnum.Vacancydescription) ?
                _enumHelper.GetEnumDescription(SetFileOriginEnum.Vacancydescription) :
                _enumHelper.GetEnumDescription(SetFileOriginEnum.PreApplicationdescription)
                , registerId.ToString() ?? string.Empty
                , code + Path.GetExtension(fileName)?.ToLower()
                 );

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("El filepath no puede ser nulo o vacío", nameof(filePath));

            if (string.IsNullOrWhiteSpace(base64File))
                throw new ArgumentException("El contenido del archivo no puede ser nulo o vacío", nameof(base64File));

            // Obtener nombre de archivo y extensión
            var extension = Path.GetExtension(filePath)?.ToLower();

            // Crear carpeta si no existe
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            // Convertir Base64 a bytes
            byte[] fileBytes;
            try
            {
                fileBytes = Convert.FromBase64String(base64File);
            }
            catch (FormatException)
            {
                throw new ArgumentException("El contenido base64 no es válido");
            }

            // Guardar archivo según tipo
            switch (extension)
            {
                case ".pdf":
                    File.WriteAllBytes(filePath, fileBytes);
                    break;

                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".bmp":
                case ".gif":
                    File.WriteAllBytes(filePath, fileBytes);
                    break;

                default:
                    throw new NotSupportedException($"El tipo de archivo '{extension}' no es soportado");
            }
            return filePath;
        }
    }
}
