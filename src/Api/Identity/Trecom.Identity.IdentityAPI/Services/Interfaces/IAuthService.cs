using Trecom.Api.Identity.Application.Helpers.JWT;
using Trecom.Api.Identity.Application.Models.Entities;

namespace Trecom.Api.Identity.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<AccessToken> CreateAccessToken(User user);
        public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress);
        public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
        public Task AddDefaultUserRoleToNewUser(User user);

    }

}
