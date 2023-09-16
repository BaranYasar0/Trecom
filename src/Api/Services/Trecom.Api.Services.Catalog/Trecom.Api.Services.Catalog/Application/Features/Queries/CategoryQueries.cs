using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.ViewModels;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Application.Features.Queries;

public record GetCategoryListQuery(QueryPaginationDto Pagination) : IQueryBase<ApiResponse<PaginationViewModel<CategoryResponseDto>>>
{
    public bool BypassCache { get; }
    public string CacheKey => QueryMethods.SetCatchKey(this);
    public TimeSpan? SlidingExpiration { get; }
}