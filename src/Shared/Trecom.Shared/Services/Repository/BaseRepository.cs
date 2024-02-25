using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Trecom.Shared.Models;
using Trecom.Shared.Services.Repository.BaseInterfaces;

namespace Trecom.Shared.Services.Repository;

public class BaseRepository<T,TContext> : IRepository<T> where T : BaseEntity where TContext : DbContext
{
    protected TContext _dbContext;

    public BaseRepository(TContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<T> dbset => _dbContext.Set<T>();


    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = dbset.AsQueryable();

        if (include != null) query = include(query);

        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<PaginationViewModel<T>> GetListAsync(int size=10,int page=1,Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = dbset.AsQueryable();

        if (include != null) query = include(query);
        if (predicate != null) query = query.Where(predicate);
        if(orderBy != null) query = orderBy(query);

        int count = await query.CountAsync();
        query = GetPaginableQuery(query, size, page - 1);
        return await new PaginationViewModel<T>().PaginableListAsync(query, count, size, page, cancellationToken);
    }

    public async Task<PaginationViewModel<T>> GetListAsNoTrackingAsync(int size = 10, int page = 1, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = dbset.AsQueryable().AsNoTracking();
        
        if (include != null) query = include(query);
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) query = orderBy(query);

        int count= await query.CountAsync();
        query = GetPaginableQuery(query,size,page-1);
        return await new PaginationViewModel<T>().PaginableListAsync(query,count ,size, page, cancellationToken);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<int> AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbContext
            .AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
        return entities.Count();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            _dbContext.Update(entity);
        }

        await _dbContext.SaveChangesAsync();
        return entities;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
            _dbContext.Attach(entity);

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(Guid id)
    {
        var entity = await dbset.FirstOrDefaultAsync(x => x.Id == id);
        return await DeleteAsync(entity);
    }

    public async Task<int> DeleteRangeAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            await DeleteAsync(entity);
        }

        return 1;
    }

    public async Task<int> DeleteRangeAsync(IEnumerable<Guid> ids)
    {
        foreach (var id in ids)
        {
            await DeleteAsync(await dbset.FirstOrDefaultAsync(x => x.Id == id));
        }

        return 1;
    }

    private IQueryable<T> GetPaginableQuery(IQueryable<T> query,int size,int page)
    {
        return query.Skip(page).Take(size);
    }
}