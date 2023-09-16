using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Aggregations;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Extensions;

public static class ElasticsearchExtension
{
    public static Task<List<T>> GetDocumentsWithMatchedId<T>(this ElasticsearchClient client, SearchResponse<T> response) where T : BaseEntity
    {
        foreach (var hit in response.Hits)
        {
            hit.Source.Id = Guid.Parse(hit.Id);
        }

        return Task.FromResult(response.Documents.ToList());
    }

    public static Task<List<T>> GetDocumentsWithMatchedId<T>(this SearchResponse<T>? response) where T : BaseEntity
    {
        if (response.NullValidation() && response.Hits.Any())
        {
            foreach (var hit in response.Hits)
            {
                hit.Source.Id = Guid.Parse(hit.Id);
            }

            return Task.FromResult(response.Documents.ToList());
        }

        return Task.FromResult(new List<T>());
    }

    public static SearchRequestDescriptor<T> ConfigurePaginationParameters<T>(this SearchRequestDescriptor<T> descriptor,
        QueryPaginationDto pagination) where T : BaseEntity
    {
        return descriptor.From((pagination.Page - 1) * pagination.PageSize)
            .Size(pagination.PageSize);
    }
}