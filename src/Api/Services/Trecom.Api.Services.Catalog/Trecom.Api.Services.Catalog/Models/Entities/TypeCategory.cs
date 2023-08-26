using System.Text.Json.Serialization;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Models.Entities;

public class TypeCategory : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? PictureUrl { get; set; }
    
    [JsonPropertyName("base_category_id")]
    public Guid BaseCategoryId { get; set; }
    [JsonPropertyName("base_category")]
    public BaseCategory BaseCategory { get; set; }

    public TypeCategory(string name, Guid baseCategoryId)
    {
        Name = name;
        BaseCategoryId = baseCategoryId;
    }
}