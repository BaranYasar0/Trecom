using Trecom.Shared.Models;

namespace Trecom.Api.Services.Order.Domain.Entities;

public class Address:ValueObject
{
    public string City { get; set; }
    public string District { get; set; }
}