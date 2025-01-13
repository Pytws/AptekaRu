using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AptekaRu.DAL.Models;

public partial class ItemsPurchase
{
    [JsonPropertyName("id_items_purchases")]
    public Guid IdItemsPurchases { get; set; }

    [JsonPropertyName("id_purchase")]
    public Guid IdPurchase { get; set; }

    [JsonPropertyName("id_drag")]
    public Guid IdDrag { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonIgnore]
    public virtual Drug IdDragNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Purchase IdPurchaseNavigation { get; set; } = null!;
}
