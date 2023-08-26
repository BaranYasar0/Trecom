using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Application.Features.Commands
{
    public record CreateProductCommand(CreateProductDto CreateProductDto) : CommandBase<CreateProductResponseDto>
    {
    }
}
