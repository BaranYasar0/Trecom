using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trecom.Api.Services.Order.Domain.Entities;

namespace Trecom.Api.Services.Order.Persistance.EntitiyConfigurations;

public class DeliveryCompanyEntityConfiguration:BaseEntityConfiguration<DeliveryCompany>
{
    public override void Configure(EntityTypeBuilder<DeliveryCompany> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.DeliveryCompany)
            .HasForeignKey(x => x.DeliveryCompanyId);
    }
}