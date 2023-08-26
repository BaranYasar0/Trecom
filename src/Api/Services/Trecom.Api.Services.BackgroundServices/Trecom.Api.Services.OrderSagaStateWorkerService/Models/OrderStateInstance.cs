using System.Text;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Trecom.Api.Services.OrderSagaStateWorkerService.Models;

public class OrderStateInstance : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }

    public Guid OrderId { get; set; }

    public Guid BuyerId { get; set; }

    public string CurrentState { get; set; }
        
    public string CardNumber { get; set; }

    [Precision(18,2)]
    public decimal TotalPrice { get; set; }

    public DateTime CreatedDate { get; set; }

    
    
    public override string ToString()
    {
        var properties = GetType().GetProperties();

        var sb = new StringBuilder();

        properties.ToList().ForEach(x =>
        {
            var value = x.GetValue(this);
            sb.AppendLine($"{x.Name}:{value}");
        });

        sb.Append("-------------------------");
        return sb.ToString();
    }
}