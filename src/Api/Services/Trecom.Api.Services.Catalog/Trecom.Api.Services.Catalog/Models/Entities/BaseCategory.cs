using System.Security.AccessControl;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Models.Entities;

public class BaseCategory : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? PictureUrl { get; set; }

    public BaseCategory(string name):base()
    {
        Name = name;
    }

    public BaseCategory()
    {
        
    }
}