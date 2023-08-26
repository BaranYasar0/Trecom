using MediatR;

namespace Trecom.Shared.Models
{
    public record CommandBase<T>:IRequest<T>
    {
    }
}
