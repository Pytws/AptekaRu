using AptekaRu.DAL.Enums;
using AptekaRu.DAL.Models;

namespace AptekaRu.DAL.Interfaces
{
    public interface IRenderTable
    {
        Task<IEnumerable<ColumnInfo>> GetColumnsInformation(string schema, TableType tableType, string tableName);
        Task<IEnumerable<TableInfo>> GetTablesInformation(string schema, TableType tableType);
        Task<IEnumerable<ConstraintInfo>> GetConstraintInfo(string schema, string tableName, ConstraintType constraintType);
    }
}
