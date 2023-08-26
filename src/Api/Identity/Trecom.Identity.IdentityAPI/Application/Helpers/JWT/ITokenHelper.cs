using Trecom.Api.Identity.Application.Models.Entities;

namespace Trecom.Api.Identity.Application.Helpers.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, IList<OperationClaim> operationClaims);

        RefreshToken CreateRefreshToken(User user, string ipAddress);
    }
}