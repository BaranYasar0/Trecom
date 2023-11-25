using Trecom.Shared.CCS.GlobalException;

namespace Trecom.Api.Services.Catalog.Persistance.Repository;

public class DatabaseException : BusinessException
{
    public DatabaseException() : base()
    {

    }
    public DatabaseException(string? message) : base(message)
    {
    }
}