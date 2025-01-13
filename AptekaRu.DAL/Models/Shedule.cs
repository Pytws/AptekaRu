using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AptekaRu.DAL.Models;

public partial class Shedule
{
    [JsonPropertyName("id_shedule")]
    public Guid IdShedule { get; set; }

    [JsonPropertyName("id_employee")]
    public Guid IdEmployee { get; set; }

    [JsonPropertyName("id_work_shedule")]
    public int IdWorkShedule { get; set; }

    [JsonPropertyName("id_pharmacie")]
    public Guid IdPharmacie { get; set; }

    [JsonPropertyName("start")]
    public DateTimeOffset Start { get; set; }

    [JsonPropertyName("end")]
    public DateTimeOffset End { get; set; }

    [JsonPropertyName("missed_hours")]
    public int MissedHours { get; set; }

    [JsonPropertyName("status")]
    public bool Status { get; set; }

    [JsonIgnore]
    public virtual Employee IdEmployeeNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Pharmacy IdPharmacieNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual WorkShedule IdWorkSheduleNavigation { get; set; } = null!;
}
