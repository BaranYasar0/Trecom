using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Trecom.Shared.Models;

namespace Trecom.Shared.Services.Repositories.BaseInterfaces;

public interface IAsyncRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, CancellationToken cancellationToken = default);

    Task<PaginationViewModel<TEntity>> GetListAsync(int size = 10, int index = 0, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = default);

    Task<PaginationViewModel<TEntity>> GetListAsNoTrackingAsync(int size = 10, int index = 0, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity);
    Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

    Task<TEntity> UpdateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities);
    Task<TEntity> DeleteAsync(TEntity entity);
    Task<TEntity> DeleteAsync(Guid id);
    Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities);
    Task<int> DeleteRangeAsync(IEnumerable<Guid> ids);
}