using Trecom.Shared.Models;

namespace Trecom.Api.Services.Order.Application.Services.Repositories.BaseInterfaces;

public interface ISyncRepository<TEntity> where TEntity : BaseEntity
{
    //TEntity? Get(Expression<Func<TEntity, bool>> predicate,
    //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);


}