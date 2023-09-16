using Trecom.Api.Services.Catalog.Models.Enums;

namespace Trecom.Api.Services.Catalog.Models.Dtos;

public class CreateProductDto
{
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }
    public Guid SupplierId { get; set; }

}