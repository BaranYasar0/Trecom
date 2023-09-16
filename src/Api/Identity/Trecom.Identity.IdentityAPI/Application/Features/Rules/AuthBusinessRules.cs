using Microsoft.EntityFrameworkCore;
using Trecom.Api.Identity.Application.Models.Entities;
using Trecom.Api.Identity.EntityFramework;

namespace Trecom.Api.Identity.Application.Features.Rules;

public class AuthBusinessRules
{
    private readonly AppDbContext _context;

    public AuthBusinessRules(AppDbContext context)
    {
        _context = context;
    }

    public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
    {
        User? user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        if (user != null)
        {
            throw new Exception($"{email} zaten kayıtlı lütfen başka bir e-mail ile kayıt olunuz!");
        }

    }

}