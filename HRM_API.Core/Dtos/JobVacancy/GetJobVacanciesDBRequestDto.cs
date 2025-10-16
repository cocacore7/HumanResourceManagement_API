namespace HRM_API.Core.Dtos.JobVacancy
{
    public class GetJobVacanciesDBRequestDto
    {
        public int Id { get; set; } = 0;
        public string Puesto { get; set; } = string.Empty;
        public string JefeSolicitante { get; set; } = string.Empty;
        public string PuestoSolicitante { get; set; } = string.Empty;
        public string AreaSolicitante { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string HubTienda { get; set; } = string.Empty;
        public string Objetivo { get; set; } = string.Empty;
        public string TipoPlaza { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public decimal Salario { get; set; } = 0;
        public int PlazasACubrir { get; set; } = 0;
        public string Comentario { get; set; } = string.Empty;
        public string RequisicionUrl { get; set; } = string.Empty;
        public string FechaPublicacion { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
