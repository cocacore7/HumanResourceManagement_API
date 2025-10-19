using System.ComponentModel;

namespace HRM_API.Core.Enum.Form
{
    public enum SetFormAnswersTypeFileEnum
    {
        [Description("text")] Text,
        [Description("textarea")] Textarea,
        [Description("number")] Number,
        [Description("currency")] Currency,
        [Description("boolean")] Boolean,
        [Description("file")] File,
        [Description("enum")] Enum,
        [Description("multienum")] Multienum,
        [Description("date")] Date,
        [Description("void")] Void
}
}