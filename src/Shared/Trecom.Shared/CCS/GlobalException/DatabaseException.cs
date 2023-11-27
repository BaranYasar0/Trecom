namespace Trecom.Shared.CCS.GlobalException;

public class DatabaseException : BusinessException
{
    public DatabaseException() : base()
    {

    }
    public DatabaseException(string? message) : base(message)
    {
    }
}