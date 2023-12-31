﻿using System.Collections.Immutable;
using AutoMapper;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Reindex;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Microsoft.Extensions.Options;
using Trecom.Api.Services.Catalog.Constants;
using Trecom.Api.Services.Catalog.Extensions;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Api.Services.Catalog.Models.Enums;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Persistance.Elasticsearch.Repository;

public class ProductElasticRepository
{
    private readonly ElasticsearchClient client;
    private ElasticIndexSettings IndexSettings;
    private readonly IMapper mapper;
    public ProductElasticRepository(ElasticsearchClient elasticClient, IOptions<ElasticIndexSettings> indexName, IMapper mapper)
    {
        client = elasticClient;
        this.mapper = mapper;
        IndexSettings = indexName.Value;
    }

    public async Task<PaginationViewModel<ProductResponseDto>> GetAllProductsAsync(QueryPaginationDto pagination)
    {
        var response = await client.SearchAsync<Product>(s => s
            .Index(IndexSettings.ProductIndexName)
            .ConfigurePaginationParameters(pagination)
            .Query(q => q
                .MatchAll()
            ));

        var productList = response.NullValidation() ? await response.GetDocumentsWithMatchedId() : new List<Product>();

        return PaginationViewModel<ProductResponseDto>.Create(mapper.Map<List<ProductResponseDto>>(productList), (int)response.Total, pagination.PageSize, pagination.Page);
    }

    public async Task<ProductResponseDto> GetProductByIdAsync(string id)
    {
        var response = await client.GetAsync<Product>(id, s => s.Index(IndexSettings.ProductIndexName));

        return mapper.Map<ProductResponseDto>(response.Source);
    }

    public async Task<PaginationViewModel<ProductResponseDto>> GetProductByIdsAsync(List<Guid> ids, QueryPaginationDto pagination
    )
    {
        List<string> stringIds = ids.Select(x => x.ToString()).ToList();

        var response = await client.SearchAsync<Product>(s => s
            .ConfigurePaginationParameters(pagination)
            .Size(pagination.PageSize)
            .Query(q => q.Ids(id =>
                id.Values(new Ids(stringIds))
            )));

        var productList = response.NullValidation() ? await response.GetDocumentsWithMatchedId() : new List<Product>();

        return PaginationViewModel<ProductResponseDto>.Create(mapper.Map<List<ProductResponseDto>>(productList), (int)response.Total, pagination.PageSize, pagination.Page);
    }
    public async Task<List<ProductResponseDto>> Test(string? name)
    {
        var response = await client.SearchAsync<Product>(s => s.Index(IndexSettings.ProductIndexName)
            .Query(q =>
                q.Fuzzy(t =>
                    t.Field(f =>
                        f.Supplier.Name.Suffix("keyword")).Value(name))));

        return mapper.Map<List<ProductResponseDto>>(response.Documents.ToList());
    }

    public async Task<PaginationViewModel<ProductResponseDto>> GetProductsByNameAsync(string requestName, QueryPaginationDto requestPagination)
    {
        var response = await client.SearchAsync<Product>(s => s.Index(IndexSettings.ProductIndexName)
            .ConfigurePaginationParameters(requestPagination)
            .Query(q =>
                q.MatchPhrase(mp =>
                    mp.Field(f =>
                        f.Name).Query(requestName))));

        if (response.IsValidResponse)
            return PaginationViewModel<ProductResponseDto>.Create(mapper.Map<List<ProductResponseDto>>(response.GetDocumentsWithMatchedId()), (int)response.Total, requestPagination.PageSize, requestPagination.Page);

        return PaginationViewModel<ProductResponseDto>.Create(new List<ProductResponseDto>(), 0, requestPagination.PageSize, requestPagination.Page);
    }

    public async Task<PaginationViewModel<ProductResponseDto>> GetProductsByCategoryNameAsync(string requestCategoryName, QueryPaginationDto requestPagination)
    {
        //var response = await client.SearchAsync<Product>(s => s.Index(IndexSettings.ProductIndexName)
        //    .ConfigurePaginationParameters(requestPagination)
        //    .Query(q =>
        //        q.MatchPhrasePrefix(mpp =>
        //            mpp.Field(f =>
        //                f.Categories).Query(requestCategoryName))));

        //if (response.IsValidResponse)
        //    return PaginationViewModel<ProductResponseDto>.Create(mapper.Map<List<ProductResponseDto>>(response.Documents.ToList()), (int)response.Total, requestPagination.PageSize, requestPagination.Page);

        //return PaginationViewModel<ProductResponseDto>.Create(new List<ProductResponseDto>(), 0, requestPagination.PageSize, requestPagination.Page);

        return null;
    }

    public async Task<PaginationViewModel<ProductResponseDto>> GetProductsByBrandIdAsync(Guid brandId,
        QueryPaginationDto pagination)
    {
        var response = await client.SearchAsync<Product>(s => s.Index(IndexSettings.ProductIndexName)
            .ConfigurePaginationParameters(pagination)
            .Query(q =>
                q.Term(t =>
                    t.Field(f =>
                        f.BrandId.Suffix("keyword")).Value(brandId.ToString()))));

        if (response.IsValidResponse)
            return PaginationViewModel<ProductResponseDto>.Create(mapper.Map<List<ProductResponseDto>>(await response.GetDocumentsWithMatchedId()), (int)response.Total, pagination.PageSize, pagination.Page);

        return PaginationViewModel<ProductResponseDto>.Create(new List<ProductResponseDto>(), 0, pagination.PageSize, pagination.Page);
    }

    public async Task<ProductResponseDto> CreateProductAsync(Product product)
    {
        var result = await client.IndexAsync(product,
            x => x.
                Index(IndexSettings.ProductIndexName).Id(product.Id));

        if (!result.IsValidResponse)
        {
            throw new DatabaseException($"{product.Name} cannot be added to Db");
        }

        return mapper.Map<ProductResponseDto>(product);
    }

    //public async Task<PaginationViewModel<ProductResponseDto>> GetProductListByQueryParameters(QuerySortCriteriasDto sortCriterias,QuerySearchCriteriasDto searchCriterias,QueryPaginationDto pagination)
    //{
    //    var response = await client.SearchAsync<Product>(s => s.Index(IndexSettings.ProductIndexName)
    //        .ConfigurePaginationParameters(pagination)
    //        .Query(q =>
    //            q.Bool(b => b

    //        )));

    //    if (!response.IsValidResponse)
    //        return PaginationViewModel<ProductResponseDto>.Create(new List<ProductResponseDto>(), response.Documents.Count);

    //    var productList = response.Documents.NullBusinessValidation()? await response.GetDocumentsWithMatchedId() : new List<Product>();

    //    return PaginationViewModel<ProductResponseDto>.Create(
    //        mapper.Map<List<ProductResponseDto>>(productList)
    //        , response.Documents.Count);
    //}

    //private Action<SortOptionsDescriptor<Product>> GetSortCriteria(QuerySortCriteriasDto sortCriterias)
    //{


    //    sortCriterias.SortBy switch
    //    {
    //        SortCriteria.LowestPrice => (q) => q.Field(f => f.Price).Ascending(),
    //        SortCriteria.HighestPrice => (q) => q.Field(f => f.Price).Descending(),
    //        SortCriteria.Newest => (q) => q.Field(f => f.CreatedDate).Descending(),
    //        SortCriteria.TopSelling => (q) => q.Field(f => f.SalesCount).Descending(),
    //        SortCriteria.TopRated => (q) => q.Field(f => f.Rating).Descending(),
    //        SortCriteria.MostReviewed => (q) => q.Field(f => f.ReviewsCount).Descending(),
    //        _ => (q) => q.Field(f => f.CreatedDate).Descending()
    //    };
    //    Action<SortOptionsDescriptor<Product>> sortCriteria = (q) => q.Field(f=>f)


    //}
}