using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.ViewModels;

namespace Trecom.Api.Services.Catalog.Application.Features.Queries
{
    public record GetBaseCategoryListQuery(QueryPaginationDto Pagination) : IQueryBase<PaginationViewModel<BaseCategoryResponseDto>>
    {
        public bool BypassCache { get; }
        public string CacheKey => QueryMethods.SetCatchKey(this);
        public TimeSpan? SlidingExpiration { get; }
    }
    public record GetTypeCategoriesByBaseCategoryIdQuery(Guid BaseCategoryId, QueryPaginationDto Pagination) : IQueryBase<PaginationViewModel<BaseCategoryResponseDto>>
    {
        public bool BypassCache { get; }
        public string CacheKey => QueryMethods.SetCatchKey(this);
        public TimeSpan? SlidingExpiration { get; }
    }
    public record GetSpecificCategoriesByTypeCategoryIdQuery(Guid TypeCategoryId, QueryPaginationDto Pagination) : IQueryBase<PaginationViewModel<BaseCategoryResponseDto>>
    {
        public bool BypassCache { get; }
        public string CacheKey => QueryMethods.SetCatchKey(this);
        public TimeSpan? SlidingExpiration { get; }
    }


}
