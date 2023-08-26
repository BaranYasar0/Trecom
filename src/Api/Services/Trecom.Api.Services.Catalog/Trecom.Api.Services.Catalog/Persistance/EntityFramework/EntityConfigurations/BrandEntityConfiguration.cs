using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trecom.Api.Services.Catalog.Models.Entities;

namespace Trecom.Api.Services.Catalog.Persistance.EntityFramework.EntityConfigurations
{
    public class BrandEntityConfiguration : BaseEntityConfiguration<Brand>
    {
        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");

            
        }
    }
}
