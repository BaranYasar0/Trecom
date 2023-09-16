using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Order.Domain.Entities;

public class Buyer:BaseEntity
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }

    public ICollection<Address> Addresses { get; set; }

    public ICollection<Order> Orders { get; set; }

    public Buyer(Guid userId, string fullName)
    {
        UserId = userId;
        FullName = fullName;
    }
}