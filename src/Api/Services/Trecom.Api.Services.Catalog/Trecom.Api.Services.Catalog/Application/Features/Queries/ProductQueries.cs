using MediatR;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Application.Features.Queries;

public record GetProductListQuery(QueryPaginationDto Pagination):IRequest<PaginationViewModel<ProductResponseDto>> /*: IQueryBase<PaginationViewModel<ProductResponseDto>>*/
{
    public bool BypassCache { get; }
    public string CacheKey => QueryMethods.SetCatchKey(this, Pagination);
    public TimeSpan? SlidingExpiration { get; }
}
public record GetProductByIdQuery(Guid Id) : IQueryBase<ProductResponseDto>
{
    public bool BypassCache { get; }
    public string CacheKey => QueryMethods.SetCatchKey(this);
    public TimeSpan? SlidingExpiration { get; }
}
public record GetProductListByIdsQuery(List<Guid> Ids, QueryPaginationDto Pagination) : IQueryBase<PaginationViewModel<ProductResponseDto>>
{
    public bool BypassCache { get; }
    public string CacheKey => QueryMethods.SetCatchKey(this, Pagination);
    public TimeSpan? SlidingExpiration { get; }
}
public record GetProductListByNameQuery(string Name, QueryPaginationDto Pagination) : IQueryBase<PaginationViewModel<ProductResponseDto>>
{
    public bool BypassCache { get; }
    public string CacheKey => QueryMethods.SetCatchKey(this, Pagination);
    public TimeSpan? SlidingExpiration { get; }
}
public record GetProductListByCategoryNameQuery(string CategoryName, QueryPaginationDto Pagination) : IQueryBase<PaginationViewModel<ProductResponseDto>>
{
    public bool BypassCache { get; }
    public string CacheKey => QueryMethods.SetCatchKey(this, this.Pagination);
    public TimeSpan? SlidingExpiration { get; }
}
public record GetProductListByBrandIdQuery(Guid brandId, QueryPaginationDto Pagination) : IQueryBase<PaginationViewModel<ProductResponseDto>>
{
    public bool BypassCache { get; }
    public string CacheKey => QueryMethods.SetCatchKey(this, Pagination);
    public TimeSpan? SlidingExpiration { get; }
}
public record GetProductListBySupplierIdQuery(Guid supplierId, QueryPaginationDto Pagination) : IQueryBase<PaginationViewModel<ProductResponseDto>>
{
    public bool BypassCache { get; }
    public string CacheKey => QueryMethods.SetCatchKey(this, Pagination);
    public TimeSpan? SlidingExpiration { get; }
}
public record GetProductListByTypeCategoryId(Guid typeCategoryId, QueryPaginationDto Pagination) : IQueryBase<PaginationViewModel<ProductResponseDto>>
{
    public bool BypassCache { get; }
    public string CacheKey => QueryMethods.SetCatchKey(this, Pagination);
    public TimeSpan? SlidingExpiration { get; }
}

public record GetProductListByQueryParameters : IRequest<PaginationViewModel<ProductResponseDto>>;
