using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trecom.Api.Services.Order.Domain.Entities;

namespace Trecom.Api.Services.Order.Persistance.EntitiyConfigurations;

public class BuyerEntityConfiguration:BaseEntityConfiguration<Buyer>
{
    public override void Configure(EntityTypeBuilder<Buyer> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x=>x.FullName)
            .HasColumnType("nvarchar(70)")
            .IsRequired();

        builder.OwnsMany(x => x.Addresses, a =>
        {
            a.WithOwner().HasForeignKey("OwnerId");
            a.Property<int>("Id");
            a.HasKey("Id");
        });

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Buyer)
            .HasForeignKey(x => x.BuyerId);
    }
}