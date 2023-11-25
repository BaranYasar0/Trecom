namespace Trecom.Api.Services.Catalog.Application.Events;

public class UpdateRelatedPropsForCreateProductEvent
{
    public string ProductId { get; set; }
    public Guid BrandId { get; set; }
    public Guid SupplierId { get; set; }
    public Guid CategoryId { get; set; }

    public UpdateRelatedPropsForCreateProductEvent(Guid brandId, Guid supplierId, string productId,Guid categoryId)
    {
        BrandId = brandId;
        SupplierId = supplierId;
        ProductId = productId;
        CategoryId = categoryId;
    }
}