using Trecom.Api.Services.Catalog.Models.Enums;

namespace Trecom.Api.Services.Catalog.Models.Dtos;

public record QuerySortCriteriasDto(SortCriteria SortBy, string? SortDirection);