using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Trecom.Shared.Services.Interfaces;

namespace Trecom.Shared.Services;

public class SharedUserService:ISharedUserService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public SharedUserService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GetUserIdAsync()
    {
        return await Task.FromResult(
            httpContextAccessor?.HttpContext?.User?.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value!);
    }
}