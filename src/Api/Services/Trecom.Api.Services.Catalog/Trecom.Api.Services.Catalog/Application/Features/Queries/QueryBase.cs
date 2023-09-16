using MediatR;
using Trecom.Shared.Pipelines.Catching;

namespace Trecom.Api.Services.Catalog.Application.Features.Queries;

public interface IQueryBase<T>:IRequest<T> where T:class
{

}