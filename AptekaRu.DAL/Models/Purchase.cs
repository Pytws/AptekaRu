using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AptekaRu.DAL.Models;

public partial class Purchase
{
    [JsonPropertyName("id_purchase")]
    public Guid IdPurchase { get; set; }

    [JsonPropertyName("id_employee")]
    public Guid IdEmployee { get; set; }

    [JsonPropertyName("id_client")]
    public Guid IdClient { get; set; }

    [JsonPropertyName("date_purchase")]
    public DateTimeOffset DatePurchase { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = null!;

    [JsonPropertyName("payment_type")]
    public string PaymentType { get; set; } = null!;

    [JsonIgnore]
    public virtual Client IdClientNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Employee IdEmployeeNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ItemsPurchase> ItemsPurchases { get; set; } = new List<ItemsPurchase>();
}
