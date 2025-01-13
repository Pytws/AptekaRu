using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AptekaRu.DAL.Models;

public partial class Image
{
    [JsonPropertyName("id_images")]
    public Guid IdImages { get; set; }

    [JsonPropertyName("id_drag")]
    public Guid IdDrag { get; set; }

    [JsonPropertyName("path_img")]
    public string PathImg { get; set; } = null!;

    [JsonIgnore]
    public virtual Drug IdDragNavigation { get; set; } = null!;
}
