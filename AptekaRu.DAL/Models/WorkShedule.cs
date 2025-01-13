using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AptekaRu.DAL.Models;

public partial class WorkShedule
{
    [JsonPropertyName("id_work_shedule")]
    public int IdWorkShedule { get; set; }

    [JsonPropertyName("work_shedule")]
    public string WorkShedule1 { get; set; } = null!;

    [JsonPropertyName("working_hours")]
    public int WorkingHours { get; set; }

    [JsonIgnore]
    public virtual ICollection<Shedule> Shedules { get; set; } = new List<Shedule>();
}
