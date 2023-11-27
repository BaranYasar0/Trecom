using Trecom.Shared.CCS.GlobalException;

namespace Trecom.Api.Services.Catalog.Persistance.Elasticsearch.Repository;

public class DatabaseException : BusinessException
{
    public DatabaseException() : base()
    {

    }
    public DatabaseException(string? message) : base(message)
    {
    }
}