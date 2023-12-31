﻿using AutoMapper;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;
using Trecom.Api.Services.Catalog.Constants;
using Trecom.Api.Services.Catalog.Extensions;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Shared.CCS.GlobalException;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Persistance.Elasticsearch.Repository;

public class CategoryElasticRepository
{
    private readonly ElasticsearchClient client;
    private readonly ElasticIndexSettings indexSettings;
    private readonly IMapper mapper;

    public CategoryElasticRepository(ElasticsearchClient client, IOptions<ElasticIndexSettings> indexSettings, IMapper mapper)
    {
        this.client = client;
        this.mapper = mapper;
        this.indexSettings = indexSettings.Value;
    }

    public async Task<CategoryResponseDto> CreateCategoryAsync(Category category)
    {
        var result =
            await client.IndexAsync(category, x => x.Index(indexSettings.CategoryIndexName).Id(category.Id));

        if (!result.IsValidResponse)
            throw new DatabaseException($"{category.Names.FirstOrDefault()} cannot be added to Db");

        return new CategoryResponseDto(category.Id, category.Names);
    }

    public async Task<PaginationViewModel<CategoryResponseDto>> GetAllCategoriesAsync(QueryPaginationDto pagination)
    {
        var response = await client.SearchAsync<Category>(s => s
            .Index(indexSettings.CategoryIndexName)
            .ConfigurePaginationParameters(pagination)
            .Query(q => q
                .MatchAll()
            ));

        var categoryList = await response.GetDocumentsWithMatchedId() ?? new List<Category>();

        return PaginationViewModel<CategoryResponseDto>.Create(mapper.Map<List<CategoryResponseDto>>(categoryList), (int)response.Total, pagination.PageSize, pagination.Page);
    }

}