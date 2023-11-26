using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Trecom.Api.Services.Order.Application.Services.Repositories.BaseInterfaces;
using Trecom.Api.Services.Order.Application.Services.Repositories.Paginate;
using Trecom.Api.Services.Order.Persistance.Contexts;
using Trecom.Shared.Models;
using Trecom.Shared.Services.Repositories.BaseInterfaces;

namespace Trecom.Api.Services.Order.Persistance.Repositories;

public class BaseRepository<T> : IAsyncRepository<T>,ISyncRepository<T> where T : BaseEntity
{
    protected OrderDbContext _dbContext;

    public BaseRepository(OrderDbContext dbContext)
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

    public async Task<PaginationViewModel<T>> GetListAsync(int size=10,int index=0,Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = dbset.AsQueryable();

        if (predicate != null) query = query.Where(predicate);
        if (include != null) query = include(query);
        if(orderBy != null) query = orderBy(query);
        return await new PaginationViewModel<T>().PaginableListAsync(query,size,index,cancellationToken);
    }

    public async Task<PaginationViewModel<T>> GetListAsNoTrackingAsync(int size = 10, int index = 0, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = dbset.AsQueryable().AsNoTracking();

        if (predicate != null) query = query.Where(predicate);
        if (include != null) query = include(query);
        if (orderBy != null) query = orderBy(query);

        return await new PaginationViewModel<T>().PaginableListAsync(query, size, index, cancellationToken);
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

}