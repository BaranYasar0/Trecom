using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trecom.Api.Services.Order.Domain.Entities;

namespace Trecom.Api.Services.Order.Persistance.EntitiyConfigurations;

public class OrderEntityConfiguration:BaseEntityConfiguration<Domain.Entities.Order>
{
    public override void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.OrderId)
            .HasColumnType("bigint")
            .HasColumnName("OrderNumber")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnType("varchar(50)")
            .IsRequired(false);

        builder.Property(x => x.OrderStatus)
            .HasConversion<int>();

        builder.HasOne(x => x.Buyer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x=>x.BuyerId);

        builder.HasOne(x => x.OrderDetail)
            .WithOne(x => x.Order)
            .HasForeignKey<Domain.Entities.Order>(x => x.OrderDetailId)
            .IsRequired();

        builder.HasOne(x => x.DeliveryCompany)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.DeliveryCompanyId)
            .IsRequired(false);
    }
}