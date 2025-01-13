using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AptekaRu.DAL.Models;

public partial class JobTitle
{
    [JsonPropertyName("id_job_titles")]
    public int IdJobTitles { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("wages")]
    public decimal Wages { get; set; }

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
