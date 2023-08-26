using Trecom.Shared.Models;

namespace Trecom.Api.Services.Order.Domain.Entities;

public class DeliveryCompany:BaseEntity
{
    public string Name { get; set; }
    public ICollection<Order> Orders { get; set; }

    public DeliveryCompany(string name)
    {
        Name = name;
    }
}