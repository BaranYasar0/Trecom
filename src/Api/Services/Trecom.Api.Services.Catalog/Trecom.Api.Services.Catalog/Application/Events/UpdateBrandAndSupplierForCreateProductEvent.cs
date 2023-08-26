namespace Trecom.Api.Services.Catalog.Application.Events
{
    public class UpdateBrandAndSupplierForCreateProductEvent
    {
        public string ProductId { get; set; }
        public Guid BrandId { get; set; }
        public Guid SupplierId { get; set; }

        public UpdateBrandAndSupplierForCreateProductEvent(Guid brandId, Guid supplierId, string productId)
        {
            BrandId = brandId;
            SupplierId = supplierId;
            ProductId = productId;
        }
    }
}
