using Trecom.Api.Services.Catalog.Models.Enums;

namespace Trecom.Api.Services.Catalog.Models.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public List<string> Categories { get; set; }
        public Guid BrandId { get; set; }
        public Guid SupplierId { get; set; }

    }
    
}
