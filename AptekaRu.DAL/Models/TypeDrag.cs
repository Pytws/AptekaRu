using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AptekaRu.DAL.Models;

public partial class TypeDrag
{
    [JsonPropertyName("id_type_drag")]
    public int IdTypeDrag { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Drug> Drugs { get; set; } = new List<Drug>();
}
