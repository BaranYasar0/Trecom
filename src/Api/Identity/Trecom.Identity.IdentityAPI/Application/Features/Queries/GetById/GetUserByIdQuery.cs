using AutoMapper;
using MediatR;
using Trecom.Api.Identity.Application.Models.Dtos;
using Trecom.Api.Identity.Application.Models.Entities;
using Trecom.Api.Identity.EntityFramework;

namespace Trecom.Api.Identity.Application.Features.Queries.GetById
{
    public class GetUserByIdQuery:IRequest<UserQueryDto>
    {
        public Guid Id { get; set; }

        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserQueryDto>
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;

            public GetUserByIdQueryHandler(AppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UserQueryDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                User user = await _context.Users.FindAsync(request.Id);
                if (user is not null)
                    return _mapper.Map<UserQueryDto>(user);

                throw new Exception($"{request.Id}'ye sahip kullanıcı bulunamadı!");

            }
        }

    }
}
