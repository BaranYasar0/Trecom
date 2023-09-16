using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Trecom.Api.Services.Catalog.Application.Features.Queries;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.ViewModels;
using Trecom.Api.Services.Catalog.Persistance.Repository;

namespace Trecom.Api.Services.Catalog.Application.Features.Handlers;

public class ProductQueryHandlers:
    IRequestHandler<GetProductListQuery,PaginationViewModel<ProductResponseDto>>,
    IRequestHandler<GetProductByIdQuery,ProductResponseDto>,
    IRequestHandler<GetProductListByIdsQuery,PaginationViewModel<ProductResponseDto>>,
    IRequestHandler<GetProductListByNameQuery,PaginationViewModel<ProductResponseDto>>,
    IRequestHandler<GetProductListByCategoryNameQuery,PaginationViewModel<ProductResponseDto>>,
    IRequestHandler<GetProductListByBrandIdQuery,PaginationViewModel<ProductResponseDto>>

{
    private readonly ProductRepository productRepository;
    private readonly IDistributedCache distributedCache;
    public ProductQueryHandlers(ProductRepository productRepository, IDistributedCache distributedCache)
    {
        this.productRepository = productRepository;
        this.distributedCache = distributedCache;
    }

    public async Task<PaginationViewModel<ProductResponseDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllProductsAsync(request.Pagination);

        return products;
    }

    public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await productRepository.GetProductByIdAsync(request.Id.ToString());
    }

    public Task<PaginationViewModel<ProductResponseDto>> Handle(GetProductListByIdsQuery request, CancellationToken cancellationToken)
    {
        var paginableProductList=productRepository.GetProductByIdsAsync(request.Ids, request.Pagination);

        return paginableProductList;
    }

    public Task<PaginationViewModel<ProductResponseDto>> Handle(GetProductListByNameQuery request,
        CancellationToken cancellationToken)
    {
        var paginableProductList=productRepository.GetProductsByNameAsync(request.Name, request.Pagination);

        return paginableProductList;
    }

    public Task<PaginationViewModel<ProductResponseDto>> Handle(GetProductListByCategoryNameQuery request, CancellationToken cancellationToken)
    {
        var paginableProductList=productRepository.GetProductsByCategoryNameAsync(request.CategoryName, request.Pagination);

        return paginableProductList;
    }

    public Task<PaginationViewModel<ProductResponseDto>> Handle(GetProductListByBrandIdQuery request, CancellationToken cancellationToken)
    {
        var paginableProductList=productRepository.GetProductsByBrandIdAsync(request.brandId, request.Pagination);
            
        return paginableProductList;
    }
}