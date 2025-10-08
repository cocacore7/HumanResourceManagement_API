namespace HRM_API.Core.Dtos.PreApplication
{
    public class GetPreApplicationsDto
    {
        public int id { get; set; } = 0;
        public string nombre { get; set; } = string.Empty;
        public string dpi { get; set; } = string.Empty;
        public int edad { get; set; } = 0;
        public string genero { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string departamento { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public string puesto { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
        public string assignedTo { get; set; } = string.Empty;
        public string ultimoGrado { get; set; } = string.Empty;
        public string fuente { get; set; } = string.Empty;
        public string hub { get; set; } = string.Empty;
        public bool esReferido { get; set; } = false;
        public string referidoPor { get; set; } = string.Empty;
    }
}
