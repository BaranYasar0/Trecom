  using System.Linq.Expressions;
  using Trecom.Shared.Models;

  namespace Trecom.Api.Services.Catalog.Persistance.Repository.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
        public Task<T> GetByIdAsync(Guid id);
        public Task<T> GetByIdAsNoTrackingAsync(Guid id);
        public Task<List<T>> GetListAsync(Expression<Func<T,bool>> predicate);
        public Task<List<T>> GetListAsNoTrackingAsync(Expression<Func<T, bool>> predicate);

    }
}
