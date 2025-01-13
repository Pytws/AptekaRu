
namespace AptekaRu.DAL.Models
{
    public class ColumnInfo
    {
        public string ColumnName { get; set; } = null!;
        public string DataType { get; set; } = null!;
        public string IsNullable { get; set; } = null!;
        public int CharacterMaximumLength { get; set; }
        public string TableName { get; set; } = null!;
    }
}
