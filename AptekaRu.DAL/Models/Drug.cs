using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AptekaRu.DAL.Models;

public partial class Drug
{
    [JsonPropertyName("id_drag")]
    public Guid IdDrag { get; set; }

    [JsonPropertyName("article")]
    public int Article { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("frontend_name")]
    public string FrontendName { get; set; } = null!;

    [JsonPropertyName("id_type_drag")]
    public int IdTypeDrag { get; set; }

    [JsonPropertyName("manufacturer")]
    public string? Manufacturer { get; set; }

    [JsonPropertyName("brand")]
    public string? Brand { get; set; }

    [JsonPropertyName("active_ingrediet")]
    public string ActiveIngrediet { get; set; } = null!;

    [JsonPropertyName("instruction")]
    public string Instruction { get; set; } = null!;

    [JsonIgnore]
    public virtual TypeDrag IdTypeDragNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    [JsonIgnore]
    public virtual ICollection<ItemsPurchase> ItemsPurchases { get; set; } = new List<ItemsPurchase>();

    [JsonIgnore]
    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
