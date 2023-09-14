using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Trecom.Api.Identity.Application.Helpers.Encryption.Hashing;
using Trecom.Api.Identity.Application.Helpers.JWT;
using Trecom.Api.Identity.Application.Models.Dtos;
using Trecom.Api.Identity.EntityFramework;
using Trecom.Api.Identity.Services.Interfaces;
using Trecom.Shared.CCS.GlobalException;
using Trecom.Shared.Models;

namespace Trecom.Api.Identity.Application.Features.Commands
{
    public class LoginCommand : IRequest<ApiResponse<RegisterResponseDto>>
    {
        public UserForLoginDto UserForLoginDto { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<RegisterResponseDto>>
        {
            private readonly AppDbContext _context;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IAuthService _authService;
            private readonly ILogger<LoginCommandHandler> _logger;


            public LoginCommandHandler(AppDbContext context, IHttpContextAccessor contextAccessor, ITokenHelper tokenHelper, IAuthService authService, ILogger<LoginCommandHandler> logger)
            {
                _context = context;
                _contextAccessor = contextAccessor;
                _authService = authService;
                _logger = logger;
            }

            public async Task<ApiResponse<RegisterResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.UserForLoginDto.Email);
                if (user is null)
                    throw new Exception($"{request.UserForLoginDto.Email}'a ait bir kullanıcı yok.");

                byte[] passwordHash, passwordSalt;

                if (!HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                    throw new BusinessException($"{request.UserForLoginDto.Password} yanlıs!");

                AccessToken accessToken = await _authService.CreateAccessToken(user);

                _logger.LogInformation($"Giriş yapıldı ve token olusturuldu.{accessToken.Token}");

                return new ApiResponse<RegisterResponseDto>
                {
                    Data = new RegisterResponseDto
                    {
                        Email = user.Email,
                        FullName = $"{user.FirstName} {user.LastName}",
                        AccessToken = accessToken.Token,
                        Expiration = accessToken.Expiration
                    },
                    Message = "Giriş Yapıldı"

                };


            }
        }
    }
}
