using Trecom.Shared.Models;

namespace Trecom.Api.Services.Order.Application.Services.Repositories.BaseInterfaces
{
    public interface IRepository<T>:IAsyncRepository<T>,ISyncRepository<T> where T:BaseEntity
    {
    }
}
