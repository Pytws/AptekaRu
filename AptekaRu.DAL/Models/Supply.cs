using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AptekaRu.DAL.Models;

public partial class Supply
{
    [JsonPropertyName("id_supplies")]
    public Guid IdSupplies { get; set; }

    [JsonPropertyName("id_pharmacy")]
    public Guid IdPharmacy { get; set; }

    [JsonPropertyName("id_drug")]
    public Guid IdDrug { get; set; }

    [JsonPropertyName("supplier")]
    public string Supplier { get; set; } = null!;

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("current_quantity")]
    public int CurrentQuantity { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("date_production")]
    public DateTime DateProduction { get; set; }

    [JsonPropertyName("date_expiration")]
    public DateTime DateExpiration { get; set; }

    [JsonPropertyName("date_delivery")]
    public DateTime DateDelivery { get; set; }

    [JsonIgnore]
    public virtual Drug IdDrugNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Pharmacy IdPharmacyNavigation { get; set; } = null!;
}
