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
        [Description("rechazoContratacion")] HiringFail
    }
}