using MediatR;
using Trecom.Api.Identity.Application.Features.Rules;
using Trecom.Api.Identity.Application.Helpers.Encryption.Hashing;
using Trecom.Api.Identity.Application.Helpers.JWT;
using Trecom.Api.Identity.Application.Models.Dtos;
using Trecom.Api.Identity.Application.Models.Entities;
using Trecom.Api.Identity.EntityFramework;
using Trecom.Api.Identity.Services.Interfaces;

namespace Trecom.Api.Identity.Application.Features.Commands
{
    public class RegisterCommand : IRequest<RegisterResponseDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }
        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
        {
            private readonly IAuthService _authService;
            private readonly AppDbContext _context;
            private readonly AuthBusinessRules _businessRules;

            public RegisterCommandHandler(IAuthService authService, AppDbContext context, AuthBusinessRules businessRules)
            {
                _authService = authService;
                _context = context;
                _businessRules = businessRules;
            }

            public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

                string date = string.Join(".", request.UserForRegisterDto.BirthDay,
                    request.UserForRegisterDto.BirthMonth, request.UserForRegisterDto.BirthYear);
                DateTime.TryParse($"{date} {DateTime.Now.TimeOfDay}", out DateTime birthDate);
                
                User newUser = new()
                {
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    Gender = request.UserForRegisterDto.Gender,
                    BirthDate = birthDate,
                    Status = true
                };

                var createdUser = await _context.Users.AddAsync(newUser);

                await _authService.AddDefaultUserRoleToNewUser(createdUser.Entity);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser.Entity);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser.Entity, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                RegisterResponseDto registeredDto = new()
                {
                    Email = request.UserForRegisterDto.Email,
                    RefreshToken = addedRefreshToken,
                    AccessToken = createdAccessToken.Token,
                    Expiration = createdAccessToken.Expiration
                };

                await _context.SaveChangesAsync();
                return registeredDto;
            }
        }
    }
}
