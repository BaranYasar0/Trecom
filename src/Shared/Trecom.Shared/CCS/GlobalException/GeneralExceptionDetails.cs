using System.Text.Json;

namespace Trecom.Shared.CCS.GlobalException;

public class GeneralExceptionDetails
{
    public string Title { get; set; }
    public string Detail { get; set; }
    public string RequestName { get; set; }
    public int StatusCode { get; set; }
    public DateTime ThrownDate { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}