using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trecom.Api.Services.Order.Domain.Entities;

namespace Trecom.Api.Services.Order.Persistance.EntitiyConfigurations
{
    public class OrderDetailEntityConfiguration:BaseEntityConfiguration<OrderDetail>
    {
        public override void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.DeliveryNumber)
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(x => x.TotalPrice)
                .HasColumnType("decimal")
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(x => x.DeliveryDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x=>x.DeliveryType)
                .IsRequired(false)
                .HasConversion<int>();

            builder.HasOne(x => x.Order)
                .WithOne(x => x.OrderDetail)
                .HasForeignKey<OrderDetail>(x => x.OrderId);

            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.OrderDetail)
                .HasForeignKey(x => x.OrderDetailId);

            builder.OwnsOne(x => x.Address,y=>y.Property(a=>a.City).IsRequired(false));
        }
    }
}
