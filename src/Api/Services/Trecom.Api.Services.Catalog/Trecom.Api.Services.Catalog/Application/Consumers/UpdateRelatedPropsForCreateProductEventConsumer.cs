using Elastic.Clients.Elasticsearch;
using MassTransit;
using Microsoft.Extensions.Options;
using Trecom.Api.Services.Catalog.Application.Events;
using Trecom.Api.Services.Catalog.Constants;
using Trecom.Api.Services.Catalog.Extensions;
using Trecom.Api.Services.Catalog.Models.Entities;

namespace Trecom.Api.Services.Catalog.Application.Consumers;

public class UpdateRelatedPropsForCreateProductEventConsumer : IConsumer<UpdateRelatedPropsForCreateProductEvent>
{
    private readonly ElasticsearchClient client;
    private readonly ILogger<UpdateRelatedPropsForCreateProductEventConsumer> logger;
    private readonly ElasticIndexSettings elasticIndexSettings;

    public UpdateRelatedPropsForCreateProductEventConsumer(ElasticsearchClient client, ILogger<UpdateRelatedPropsForCreateProductEventConsumer> logger, IOptions<ElasticIndexSettings> elasticIndexSettings)
    {
        this.client = client;
        this.logger = logger;
        this.elasticIndexSettings = elasticIndexSettings.Value;
    }

    public async Task Consume(ConsumeContext<UpdateRelatedPropsForCreateProductEvent> context)
    {
        var brandResponse = await client.GetAsync<Brand>(index: elasticIndexSettings.
            BrandIndexName, id: context.Message.BrandId);

        Brand toBeAddedBrand = new();
        Supplier toBeAddedSupplier = new();
        Category toBeAddedCategory = new();

        if (brandResponse.NullBusinessValidation() && brandResponse.IsValidResponse && brandResponse.Source.NullBusinessValidation())
        {
            toBeAddedBrand = brandResponse.Source;
        }

        var supplierResponse = await client.GetAsync<Supplier>(index: elasticIndexSettings.
            SupplierIndexName, id: context.Message.SupplierId);

        if (supplierResponse.NullBusinessValidation() && supplierResponse.IsValidResponse && supplierResponse.Source.NullBusinessValidation())
        {
            toBeAddedSupplier = supplierResponse.Source;
        }

        var categoryResponse = await client.GetAsync<Category>(index: elasticIndexSettings.
                       CategoryIndexName, id: context.Message.CategoryId);

        if (categoryResponse.NullBusinessValidation() && categoryResponse.IsValidResponse &&
            supplierResponse.Source.NullBusinessValidation())
        {
           toBeAddedCategory= categoryResponse.Source;
        }
        
        var productResponse = await client.GetAsync<Product>(index: elasticIndexSettings.
            ProductIndexName, id: context.Message.ProductId);

        if (productResponse.NullBusinessValidation() && productResponse.IsValidResponse)
        {
            Product? toBeUpdatedProduct = productResponse.Source;
            toBeUpdatedProduct.Brand = toBeAddedBrand;
            toBeUpdatedProduct.Supplier = toBeAddedSupplier;
            toBeUpdatedProduct.Category = toBeAddedCategory;

            try
            {
                var productAddedResponse = await client.UpdateAsync<Product, Product>(index: elasticIndexSettings.ProductIndexName, id: toBeUpdatedProduct.Id, cfg =>
                {
                    cfg.Doc(toBeUpdatedProduct);
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error while updating product");
                throw;
            }

        }


    }
}