using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Models.Entities;

public class SpecificCategory : BaseEntity
{
    public string Name { get; set; }
    public string? PictureUrl { get; set; }

    public SpecificCategory(string name, Guid typeCategoryId)
    {
        Name = name;
        TypeCategoryId = typeCategoryId;
    }

    public Guid TypeCategoryId { get; set; }
    public TypeCategory TypeCategory { get; set; }
}