using Microsoft.EntityFrameworkCore;
using Trecom.Api.Identity.Application.Helpers.JWT;
using Trecom.Api.Identity.Application.Models.Entities;
using Trecom.Api.Identity.EntityFramework;
using Trecom.Api.Identity.Services.Interfaces;

namespace Watchflix.Api.Identity.Services;

public class AuthService:IAuthService
{

    private readonly ITokenHelper _tokenHelper;
    private readonly AppDbContext _context;
    private readonly ILogger<AuthService> _logger;
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthService(ITokenHelper tokenHelper, AppDbContext context, ILogger<AuthService> logger, IHttpContextAccessor contextAccessor)
    {
        _tokenHelper = tokenHelper;
        _context = context;
        _logger = logger;
        _contextAccessor = contextAccessor;
    }

    public async Task<AccessToken> CreateAccessToken(User user)
    {

        List<UserOperationClaim> userOperationClaims = await _context.UserOperationClaims
            .Where(u => u.UserId == user.Id).Include(i => i.OperationClaim).ToListAsync();

        List<OperationClaim> operationClaims = userOperationClaims.Select(x => new OperationClaim()
            { Id = x.OperationClaim.Id, Name = x.OperationClaim.Name }).ToList();

        AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);

        return accessToken;
    }

    public async Task AddDefaultUserRoleToNewUser(User user)
    {
        UserOperationClaim userClaim = new()
            { OperationClaim = await _context.OperationClaims.FindAsync(2), User = user, UserId = user.Id };

        await _context.UserOperationClaims.AddAsync(userClaim);
        await _context.SaveChangesAsync();
    }


    public async Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
        return await Task.FromResult(refreshToken);
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        var addedToken=await _context.RefreshTokens.AddAsync(refreshToken);
            
        return addedToken.Entity;
    }

       
}