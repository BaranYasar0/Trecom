using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Trecom.ServiceBus.BusinessAction.Domain;

public class IntegrationEvent
{
    public Guid Id { get; }
    public DateTime CreatedDate { get; }
    public string CreatedBy { get; set; }
    public Dictionary<string, string> Header { get; set; }

    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.Now;
    }

    [JsonConstructor]
    public IntegrationEvent(Guid id, DateTime createdDate, string createdBy, Dictionary<string, string> header = null)
    {
        Id = id;
        CreatedDate = createdDate;
        CreatedBy = createdBy;
        Header = header;
    }
    public override string ToString()
    {
        StringBuilder sb = new();
        foreach (var property in GetType().GetProperties())
        {
            sb.AppendLine($"{property.Name} : {property.GetValue(this)}");
        }
        return sb.ToString();
    }
}
