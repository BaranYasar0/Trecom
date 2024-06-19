using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Trecom.ServiceBus.Domain;

public class IntegrationEvent
{
    public Guid Id { get; }
    public DateTime CreatedDate { get; }
    public string CreatedBy { get; set; }

    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.Now;
    }

    [JsonConstructor]
    public IntegrationEvent(Guid id,DateTime createdDate,string createdBy)
    {
        Id = id;
        CreatedDate = createdDate;
        CreatedBy = createdBy;
    }
}
