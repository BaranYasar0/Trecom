using Trecom.Api.Identity.Application.Helpers.JWT;
using Trecom.Api.Identity.Application.Models.Entities;

namespace Trecom.Api.Identity.Application.Models.Dtos
{
    public class RegisterResponseDto
    {
        public string Email { get; set; }
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
