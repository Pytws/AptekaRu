using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AptekaRu.DAL.Models;

public partial class Client
{
    [JsonPropertyName("id_client")]
    public Guid IdClient { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = null!;

    [JsonPropertyName("middle_name")]
    public string MiddleName { get; set; } = null!;

    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = null!;

    [JsonPropertyName("birthday")]
    public DateTime Birthday { get; set; }

    [JsonPropertyName("sex")]
    public bool Sex { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("number_phone")]
    public string NumberPhone { get; set; } = null!;

    [JsonPropertyName("confirmed_phone")]
    public bool ConfirmedPhone { get; set; }

    [JsonIgnore]
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
