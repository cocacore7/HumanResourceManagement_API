using System.ComponentModel;

namespace HRM_API.Core.Enum.JobVacancy
{
    public enum SetJobVacancyCodeEnum
    {
        [Description("NOMBRE_PUESTO")]          JobPositionName,
        [Description("NOMBRE_COMPLETO")]        RequesterName,
        [Description("PUESTO_SOLICITANTE")]     RequesterPosition,
        [Description("AREA_SOLICITANTE")]       AreaText,
        [Description("REGION")]                 RegionText,
        [Description("HUB_TIENDA")]             HubText,
        [Description("OBJETIVO")]               Objective,
        [Description("COMENTARIO")]             Comment,
        [Description("TIPO_PLAZA")]             VacancyTypeId,
        [Description("MOTIVO")]                 ReasonId,
        [Description("SALARIO")]                Salary,
        [Description("NO_PLAZAS")]              TotalPositions,
        [Description("REQUISICION")]            RequisitionFileId
    }
}