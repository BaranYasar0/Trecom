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
    public class OrderItemEntityConfiguration:BaseEntityConfiguration<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.ProductName)
                .HasColumnType("nvarchar(70)")
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("decimal")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasOne(x => x.OrderDetail)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderDetailId);
        }
    }
}
