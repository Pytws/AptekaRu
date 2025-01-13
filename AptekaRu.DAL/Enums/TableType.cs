using System.ComponentModel;

namespace AptekaRu.DAL.Enums
{
    public enum TableType
    {
        [Description("BASE TABLE")]
        BaseTable,
        [Description("VIEW")]
        View,
        [Description("FOREIGN TABLE")]
        ForeignTable,
        [Description("LOCAL TEMPORARY")]
        LocalTemporary
    }
}
