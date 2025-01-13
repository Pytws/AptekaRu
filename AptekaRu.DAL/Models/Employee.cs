using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AptekaRu.DAL.Models;

public partial class Employee
{
    [JsonPropertyName("id_employee")]
    public Guid IdEmployee { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = null!;

    [JsonPropertyName("middle_name")]
    public string MiddleName { get; set; } = null!;

    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = null!;

    [JsonPropertyName("sex")]
    public bool Sex { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("number_phone")]
    public string NumberPhone { get; set; } = null!;

    [JsonPropertyName("id_job_title")]
    public int? IdJobTitle { get; set; }

    [JsonPropertyName("works_with")]
    public DateTime? WorksWith { get; set; }

    [JsonIgnore]
    public virtual JobTitle? IdJobTitleNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    [JsonIgnore]
    public virtual ICollection<Shedule> Shedules { get; set; } = new List<Shedule>();
}
