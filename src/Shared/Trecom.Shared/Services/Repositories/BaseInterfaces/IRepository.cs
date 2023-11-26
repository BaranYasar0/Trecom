using Trecom.Shared.Models;
using Trecom.Shared.Services.Repositories.BaseInterfaces;

namespace Trecom.Api.Services.Order.Application.Services.Repositories.BaseInterfaces;

public interface IRepository<T>:IAsyncRepository<T>,ISyncRepository<T> where T:BaseEntity
{
}