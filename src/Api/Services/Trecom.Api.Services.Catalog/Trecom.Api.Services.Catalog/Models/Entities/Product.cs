using System;
using System.Text.Json.Serialization;
using Trecom.Api.Services.Catalog.Models.Enums;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Models.Entities;

public class Product : BaseEntity
{
    //PROPERTIES
    public string Name { get; set; }
    public string? Description { get; set; }
        
    [JsonPropertyName("picture_url")]
    public string? PictureUrl { get; set; }
        
    [JsonPropertyName("unit_price")]
    public decimal UnitPrice { get; set; }

    //ENUMS
    [JsonPropertyName("body_type")]
    public BodyType? BodyType { get; set; }
    public Gender? Gender { get; set; }
    [JsonPropertyName("color_type")]
    public ColorType? Color { get; set; }

    //RELATIONS
    [JsonPropertyName("category_id")]
    public Guid CategoryId { get; set; }
    [JsonPropertyName("supplier_id")]
    public Guid SupplierId { get; set; }
    [JsonPropertyName("brand_id")]
    public Guid BrandId { get; set; }

    [JsonPropertyName("specific_category")]
    public SpecificCategory SpecificCategory { get; set; }
    public Supplier Supplier { get; set; }
    public Brand Brand { get; set; }

    public Product()
    {
            
    }

    public Product(string name,Guid specificCategoryId,Guid supplierId,Guid brandId)
    {
        Name=name;
            
    }

}