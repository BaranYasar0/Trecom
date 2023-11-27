using Microsoft.EntityFrameworkCore;
using Trecom.Shared.Models;

namespace Trecom.Shared.Services.Repository.BaseInterfaces;

public interface IRepository<T>:IAsyncRepository<T>,ISyncRepository<T> where T:BaseEntity 
{
}