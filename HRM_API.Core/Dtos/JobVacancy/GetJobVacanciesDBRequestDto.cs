namespace HRM_API.Core.Dtos.JobVacancy
{
    public class GetJobVacanciesDBRequestDto
    {
        public int id { get; set; } = 0;
        public string puesto { get; set; } = string.Empty;
        public string jefeSolicitante { get; set; } = string.Empty;
        public string puestoSolicitante { get; set; } = string.Empty;
        public string areaSolicitante { get; set; } = string.Empty;
        public string region { get; set; } = string.Empty;
        public string hubTienda { get; set; } = string.Empty;
        public string objetivo { get; set; } = string.Empty;
        public string tipoPlaza { get; set; } = string.Empty;
        public string motivo { get; set; } = string.Empty;
        public decimal salario { get; set; } = 0;
        public int plazasACubrir { get; set; } = 0;
        public string comentario { get; set; } = string.Empty;
        public string requisicionUrl { get; set; } = string.Empty;
        public string fechaPublicacion { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
    }
}
