using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;
using Trecom.Api.Services.Catalog.Constants;
using Trecom.Api.Services.Catalog.Models.Entities;

namespace Trecom.Api.Services.Catalog.Persistance.Elasticsearch.Repository;

public class BrandElasticRepository
{
    private readonly ElasticsearchClient client;
    private readonly ElasticIndexSettings elasticIndexSettings;
    public BrandElasticRepository(ElasticsearchClient client, IOptions<ElasticIndexSettings> elasticIndexSettings)
    {
        this.client = client;
        this.elasticIndexSettings = elasticIndexSettings.Value;
    }

    public async Task<Brand> CreateBrandAsync(Brand brand)
    {
        try
        {
            var response = await client.IndexAsync(brand, elasticIndexSettings.BrandIndexName);
            return brand;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Supplier> CreateSupplierAsync(Supplier supplier)
    {
        try
        {
            var response = await client.IndexAsync(supplier, elasticIndexSettings.SupplierIndexName);
            return supplier;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}