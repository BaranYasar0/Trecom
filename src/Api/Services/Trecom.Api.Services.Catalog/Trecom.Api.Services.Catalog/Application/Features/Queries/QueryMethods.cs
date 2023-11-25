using Trecom.Api.Services.Catalog.Models.Dtos;

namespace Trecom.Api.Services.Catalog.Application.Features.Queries;

internal static class QueryMethods
{
    internal static string SetCatchKey(object obj, QueryPaginationDto pagination=null)
    {
        var className = (obj.GetType().Name);

        return pagination ==null? className.Contains("Query") ? className.Split("Query")[0]:className
            : className.Contains("Query") ?
                className.Split("Query")[0]
                + pagination.PageSize.ToString()
                +"/"
                + pagination.Page.ToString()
                : className;
    }
}