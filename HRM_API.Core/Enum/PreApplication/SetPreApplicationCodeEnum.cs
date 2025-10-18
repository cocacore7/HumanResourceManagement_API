using System.ComponentModel;

namespace HRM_API.Core.Enum.PreApplication
{
    public enum SetPreApplicationCodeEnum
    {
        [Description("NOMBRE_COMPLETO")] FullName,
        [Description("DPI")] DPI,
        [Description("EDAD")] Age,
        [Description("GENERO")] Gender,
        [Description("TELEFONO")] Phone,
        [Description("EMAIL")] Email,
        [Description("DEPARTAMENTO")] Town,
        [Description("DIRECCION")] Address,
        [Description("ULTIMO_GRADO")] EducationLevel,
        [Description("PUESTO_APLICA")] Vacancy,
        [Description("TIENE_EXPERIENCIA")] Experience,
        [Description("COMO_TE_ENTERASTE")] HowHeard,
        [Description("ADJUNTA_CV")] File,
        [Description("ACEPTA_TERMINOS")] AcceptedTerms
    }
}