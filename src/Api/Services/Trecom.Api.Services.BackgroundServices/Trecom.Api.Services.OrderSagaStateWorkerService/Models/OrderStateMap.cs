using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Trecom.Api.Services.OrderSagaStateWorkerService.Models;

public class OrderStateMap : SagaClassMap<OrderStateInstance>
{
    protected override void Configure(EntityTypeBuilder<OrderStateInstance> entity, ModelBuilder model)
    {
        entity.Property(x => x.BuyerId).HasMaxLength(256);
    }
}