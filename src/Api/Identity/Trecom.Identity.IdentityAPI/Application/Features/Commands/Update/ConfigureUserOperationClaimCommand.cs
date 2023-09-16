

using MediatR;
using Trecom.Api.Identity.Application.Models.Dtos;
using Trecom.Api.Identity.Application.Models.Entities;
using Trecom.Api.Identity.EntityFramework;

namespace Trecom.Api.Identity.Application.Features.Commands.Update;

public class ConfigureUserOperationClaimCommand:IRequest<ConfigureClaimResponseDto>
{
    public Guid UserId { get; set; }
    public int OperationClaimId { get; set; }

    public class ConfigureUserOperationClaimCommandHandler:IRequestHandler<ConfigureUserOperationClaimCommand, ConfigureClaimResponseDto>
    {
        private readonly AppDbContext _context;

        public ConfigureUserOperationClaimCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ConfigureClaimResponseDto> Handle(ConfigureUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            UserOperationClaim userOperationClaim = new UserOperationClaim() { UserId = request.UserId,OperationClaimId = request.OperationClaimId};

            var entity = await _context.UserOperationClaims.AddAsync(userOperationClaim);
            await _context.SaveChangesAsync();
            if (entity != null)
            {
                return new ConfigureClaimResponseDto()
                {
                    UserId = entity.Entity.UserId,
                    OperationClaimId = entity.Entity.OperationClaimId
                };
            }

            return null;
        }
    }
}