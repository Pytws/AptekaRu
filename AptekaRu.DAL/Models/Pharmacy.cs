using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AptekaRu.DAL.Models;

public partial class Pharmacy
{
    [JsonPropertyName("id_pharmacy")]
    public Guid IdPharmacy { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("address")]
    public string Address { get; set; } = null!;

    [JsonPropertyName("operating_mode")]
    public string OperatingMode { get; set; } = null!;

    [JsonPropertyName("phones")]
    public string Phones { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Shedule> Shedules { get; set; } = new List<Shedule>();

    [JsonIgnore]
    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
