using System.Text.Json.Serialization;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Models.Entities;

public class Supplier : BaseEntity
{
    public string Name { get; set; }
    
    [JsonPropertyName("picture_url")]
    public string? PictureUrl { get; set; }
    
    [JsonPropertyName("bill_status")]
    public bool BillStatus { get; set; } = false;
    
    public Address Address { get; set; }

    public Supplier()
    {
        
    }

    public Supplier(string name)
    {
        Name = name;
    }
}