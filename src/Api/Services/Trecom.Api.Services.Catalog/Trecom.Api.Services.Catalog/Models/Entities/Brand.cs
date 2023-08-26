using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Models.Entities;

public class Brand:BaseEntity
{
    public string Name { get; set; }

    public Brand()
    {
        
    }

    public Brand(string name)
    {
        Name = name;
    }
}