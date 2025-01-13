using AptekaRu.DAL.Models;

namespace AptekaRu.Web.Models
{
    public class TableModel
    {
        public readonly IEnumerable<ColumnInfo> columnsInfo;
        public readonly IDictionary<string, object>? columnsValue;

        public TableModel(
            IEnumerable<ColumnInfo> columnsInfo, 
            IDictionary<string, object>? columnsValue)
        {
            this.columnsInfo = columnsInfo;
            this.columnsValue = columnsValue;
        }
    }
}
