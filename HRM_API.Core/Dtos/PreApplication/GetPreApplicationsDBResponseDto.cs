namespace HRM_API.Core.Dtos.PreApplication
{
    public class GetPreApplicationsDBResponseDto
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public string Dpi { get; set; } = string.Empty;
        public int Edad { get; set; } = 0;
        public string Genero { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Puesto { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = string.Empty;
        public string UltimoGrado { get; set; } = string.Empty;
        public string Fuente { get; set; } = string.Empty;
        public string Hub { get; set; } = string.Empty;
        public bool? EsReferido { get; set; } = null;
        public string ReferidoPor { get; set; } = string.Empty;
    }
}
