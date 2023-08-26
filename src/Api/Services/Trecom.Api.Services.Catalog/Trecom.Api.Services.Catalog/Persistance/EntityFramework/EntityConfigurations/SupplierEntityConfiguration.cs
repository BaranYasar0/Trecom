using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trecom.Api.Services.Catalog.Models.Entities;

namespace Trecom.Api.Services.Catalog.Persistance.EntityFramework.EntityConfigurations
{
    public class SupplierEntityConfiguration : BaseEntityConfiguration<Supplier>
    {
        public override void Configure(EntityTypeBuilder<Supplier> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(x => x.Address, x =>
            {
                x.WithOwner();
            });

            builder.Property(x => x.BillStatus)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(x => x.Name)
                .HasColumnType("varchar(50)")
                .IsRequired();
            builder.Property(x => x.PictureUrl)
                .HasColumnType("varchar(100)")
                .IsRequired(false);

            //builder.HasOne(x => x.Address)
            //    .WithOne()
            //    .HasForeignKey<Supplier>(x => x.AddressId).IsRequired();

            
        }
    }
}
