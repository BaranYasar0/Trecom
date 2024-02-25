using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Trecom.Api.Services.Catalog.Application.Features.Queries;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Api.Services.Catalog.Persistance.Elasticsearch.Repository;
using Trecom.Api.Services.Catalog.Persistance.EntityFramework.Repository.Interfaces;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Application.Features.Handlers;

public class ProductQueryHandlers:
    IRequestHandler<GetProductListQuery,PaginationViewModel<ProductResponseDto>>,
    IRequestHandler<GetProductByIdQuery,ProductResponseDto>,
    IRequestHandler<GetProductListByIdsQuery,PaginationViewModel<ProductResponseDto>>,
    IRequestHandler<GetProductListByNameQuery,PaginationViewModel<ProductResponseDto>>,
    IRequestHandler<GetProductListByCategoryNameQuery,PaginationViewModel<ProductResponseDto>>,
    IRequestHandler<GetProductListByBrandIdQuery,PaginationViewModel<ProductResponseDto>>,
    IRequestHandler<GetProductListByQueryParameters,PaginationViewModel<ProductResponseDto>>

{
    private readonly IProductRepository productRepository;
    private readonly IDistributedCache distributedCache;
    private readonly IMapper mapper;
    public ProductQueryHandlers(IProductRepository productRepository, IDistributedCache distributedCache, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.distributedCache = distributedCache;
        this.mapper = mapper;
    }

    public async Task<PaginationViewModel<ProductResponseDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetListAsNoTrackingAsync(request.Pagination.PageSize,request.Pagination.Page);

         List<ProductResponseDto> mappedProducts= mapper.Map<List<ProductResponseDto>>(products.Items);

         return PaginationViewModel<ProductResponseDto>.Create(mappedProducts,products.Count,request.Pagination.PageSize,request.Pagination.Page);
    }

    public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<ProductResponseDto>(await productRepository.GetAsync(x=>x.Id==request.Id));
    }

    public async Task<PaginationViewModel<ProductResponseDto>> Handle(GetProductListByIdsQuery request, CancellationToken cancellationToken)
    {

        var paginableProductList = await productRepository.GetListAsNoTrackingAsync(request.Pagination.PageSize, 
            request.Pagination.Page,
        x=>request.Ids.Contains(x.Id)
        );

        List<ProductResponseDto> mappedProducts = mapper.Map<List<ProductResponseDto>>(paginableProductList.Items);

        return PaginationViewModel<ProductResponseDto>.Create(mappedProducts, paginableProductList.Count, request.Pagination.PageSize, request.Pagination.Page);
    }

    public async Task<PaginationViewModel<ProductResponseDto>> Handle(GetProductListByNameQuery request,
        CancellationToken cancellationToken)
    {
        var paginableProductList = await productRepository.GetListAsNoTrackingAsync(
            request.Pagination.PageSize,
            request.Pagination.Page,
            x=>x.Name.Contains(request.Name)
        );

        List<ProductResponseDto> mappedProducts = mapper.Map<List<ProductResponseDto>>(paginableProductList.Items);

        return PaginationViewModel<ProductResponseDto>.Create(mappedProducts, paginableProductList.Count, request.Pagination.PageSize, request.Pagination.Page);
    }

    public async Task<PaginationViewModel<ProductResponseDto>> Handle(GetProductListByCategoryNameQuery request, CancellationToken cancellationToken)
    {
        var paginableProductList = await productRepository.GetListAsNoTrackingAsync(
            request.Pagination.PageSize,
            request.Pagination.Page,
            x => request.CategoryName.Contains(x.SpecificCategory.Name),
            inc=>inc.Include(y=>y.SpecificCategory)
        );

        List<ProductResponseDto> mappedProducts = mapper.Map<List<ProductResponseDto>>(paginableProductList.Items);

        return PaginationViewModel<ProductResponseDto>.Create(mappedProducts, paginableProductList.Count, request.Pagination.PageSize, request.Pagination.Page);
    }

    public async Task<PaginationViewModel<ProductResponseDto>> Handle(GetProductListByBrandIdQuery request, CancellationToken cancellationToken)
    {
        var paginableProductList = await productRepository.GetListAsNoTrackingAsync(
            request.Pagination.PageSize,
            request.Pagination.Page,
            x => x.Brand.Id==request.brandId,
            inc=>inc.Include(y=>y.Brand)
        );

        List<ProductResponseDto> mappedProducts = mapper.Map<List<ProductResponseDto>>(paginableProductList.Items);

        return PaginationViewModel<ProductResponseDto>.Create(mappedProducts, paginableProductList.Count, request.Pagination.PageSize, request.Pagination.Page);
    }

    //public async Task<PaginationViewModel<ProductResponseDto>> Handle(GetProductListByQueryParameters request, CancellationToken cancellationToken)
    //{
    //    var productList = await productRepository.GetProductListByQueryParameters();

    //    return productList;
    //}
    public async Task<PaginationViewModel<ProductResponseDto>> Handle(GetProductListByQueryParameters request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}