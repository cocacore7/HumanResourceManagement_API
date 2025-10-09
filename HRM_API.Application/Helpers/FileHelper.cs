namespace HRM_API.Application.Helpers
{
    public class FileHelper
    {
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
    }
}
