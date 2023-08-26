using Microsoft.EntityFrameworkCore;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Order.Application.Services.Repositories.Paginate
{
    public class Paginable<T> where T:BaseEntity
    {
        public int Size { get; set; }
        public int Index { get; set; }
        public List<T> Items { get; set; }
        public int Count { get; set; }


        public async Task<Paginable<T>> PaginableListAsync(IQueryable<T> items, int size, int index, CancellationToken cancellationToken = default)
        {
            Paginable<T> page = new()
            {
                Size = size,
                Index = index,
                Count = await items.CountAsync(cancellationToken),
                Items = await items.Skip(size * index).Take(size).ToListAsync(cancellationToken)
            };
            
            return page;
        }
    }
}
