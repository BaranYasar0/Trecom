using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Api.Services.Catalog.Models.Enums;

namespace Trecom.Api.Services.Catalog.Persistance.EntityFramework.EntityConfigurations;

public class ProductEntityConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Navigation(x => x.Brand).AutoInclude();

        builder.Ignore(x => x.BodyType);
        builder.Ignore(x => x.Gender);
        builder.Ignore(x => x.Color);

        builder.Property(p => p.BodyType)
            .HasConversion(
                p => p.Value,
                p => BodyType.FromValue(p)).IsRequired(false);
        builder.Property(x => x.Gender)
            .HasConversion(x => x.Value,
                x => Gender.FromValue(x)).IsRequired(false);
        builder.Property(x => x.Color)
            .HasConversion(x => x.Value,
                x => ColorType.FromValue(x)).IsRequired(false);

        builder.Property(x => x.Name)
            .HasColumnName("NAME")
            .HasColumnType("nvarchar(100)");
        builder.Property(x => x.Description)
            .HasColumnName("DESCRIPTION")
            .IsRequired(false);
        builder.Property(x => x.PictureUrl)
            .HasColumnName("PICURL")
            .HasColumnType("nvarchar(100)")
            .IsRequired(false);
        builder.Property(x => x.UnitPrice)
            .HasColumnType("decimal")
            .HasPrecision(15, 2);

           

            
    }
}