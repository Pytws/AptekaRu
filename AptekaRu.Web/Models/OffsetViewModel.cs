using System.Text.Json.Serialization;

namespace AptekaRu.Web.Models
{
    public class OffsetViewModel
    {
        [JsonPropertyName("offset")]
        public int Offset { get; set; }
        [JsonPropertyName("schemaName")]
        public string SchemaName { get; set; } = null!;
        [JsonPropertyName("tableName")]
        public string TableName { get; set; } = null!;
        [JsonPropertyName("rows")]
        public IEnumerable<IDictionary<string, object>> Rows = null!;
    }
}
