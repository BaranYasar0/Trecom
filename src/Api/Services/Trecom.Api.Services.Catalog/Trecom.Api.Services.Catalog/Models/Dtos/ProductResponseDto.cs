using Trecom.Api.Services.Catalog.Models.Enums;

namespace Trecom.Api.Services.Catalog.Models.Dtos;

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public BrandResponseDto Brand { get; set; }
    public SupplierResponseDto Supplier { get; set; }
    public SpecificCategoryResponseDto Category { get; set; }

    public ProductResponseDto()
    {
        
    }
}