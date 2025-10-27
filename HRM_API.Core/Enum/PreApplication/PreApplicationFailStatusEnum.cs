using System.ComponentModel;

namespace HRM_API.Core.Enum.PreApplication
{
    public enum PreApplicationFailStatusEnum
    {
        [Description("rechazoPreFiltro")] PreFilterFail,
        [Description("rechazoSolicitud")] RequestFail,
        [Description("rechazoRevision")] ReviewFail,
        [Description("rechazoEntrevista")] InterviewFail,
        [Description("rechazoPruebas")] TestFail,
        [Description("rechazoEntrevistaJefe")] BossInterviewFail,
        [Description("rechazoPoligrafo")] PoligraphFail,
        [Description("rechazoCargaExpediente")] ExpLoadFail,
        [Description("rechazoContratacion")] HiringFail,
        [Description("preFiltro")] PreFilter,
        [Description("solicitud")] Request,
        [Description("entrevista")] Interview,
        [Description("pruebas")] Test,
        [Description("entrevistaJefe")] BossInterview,
        [Description("poligrafo")] Poligrahp,
        [Description("cargaExpedienteNoCargado")] ExpNotLoaded,
        [Description("cargaExpedienteCompletado")] ExpLoaded
    }
}