using System.ComponentModel;

namespace HRM_API.Core.Enum.Form
{
    public enum UpdateFormAnswersEmailStatusEnum
    {
        [Description("preFiltro")] PreFilter,
        [Description("solicitud")] Request,
        [Description("entrevista")] Interview,
        [Description("pruebas")] Test,
        [Description("rechazoEntrevista")] InterviewFail,
        [Description("entrevistaJefe")] BossInterview,
        [Description("poligrafo")] Poligrahp,
        [Description("cargaExpedienteNoCargado")] ExpNotLoaded,
        [Description("cargaExpedienteCompletado")] ExpLoaded
    }
}