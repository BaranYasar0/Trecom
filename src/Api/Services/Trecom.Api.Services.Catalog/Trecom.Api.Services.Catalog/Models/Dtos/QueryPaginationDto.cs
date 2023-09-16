namespace Trecom.Api.Services.Catalog.Models.Dtos;

public class QueryPaginationDto
{
    public int PageSize { get; set; } = 10;
    public int Page { get; set; } = 1;
}