using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Trecom.Shared.Models;

public class BaseEntity
{
    public virtual Guid Id { get; set; }
    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; } = true;
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
    [JsonPropertyName("update_date")]
    public DateTime? UpdatedDate { get; set; }

    public BaseEntity()
    {
        this.Id = Guid.NewGuid();
        CreatedDate = DateTime.Now;
    }

    public BaseEntity(Guid id) : this()
    {
        Id = id;
    }

        
}