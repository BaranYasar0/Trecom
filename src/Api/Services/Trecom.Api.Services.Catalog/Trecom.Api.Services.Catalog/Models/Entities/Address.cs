using Microsoft.EntityFrameworkCore;

namespace Trecom.Api.Services.Catalog.Models.Entities;

public record Address
{
    public string City { get; set; }
    public string District { get; set; }
    public string Province { get; set; }

}