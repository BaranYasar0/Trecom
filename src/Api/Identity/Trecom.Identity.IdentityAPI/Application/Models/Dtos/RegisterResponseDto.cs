using Trecom.Api.Identity.Application.Helpers.JWT;
using Trecom.Api.Identity.Application.Models.Entities;

namespace Trecom.Api.Identity.Application.Models.Dtos;

public class RegisterResponseDto
{
    public string Email { get; set; }
    public string AccessToken { get; set; }
    public string FullName { get; set; }
    public DateTime Expiration { get; set; }
    public RefreshToken RefreshToken { get; set; }
}