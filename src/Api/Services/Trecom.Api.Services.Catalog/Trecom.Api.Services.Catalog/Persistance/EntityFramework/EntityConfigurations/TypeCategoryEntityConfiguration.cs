using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Api.Services.Catalog.Persistance.DataSeeding;

namespace Trecom.Api.Services.Catalog.Persistance.EntityFramework.EntityConfigurations;

public class TypeCategoryEntityConfiguration : BaseEntityConfiguration<TypeCategory>
{

    public override void Configure(EntityTypeBuilder<TypeCategory> builder)
    {

        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasColumnType("varchar(50)");
        builder.Property(x => x.Description)
            .IsRequired(false);
        builder.Property(x => x.PictureUrl)
            .HasColumnType("varchar(100)").IsRequired(false);

        //builder.HasMany(x => x.SpecificCategories)
        //    .WithOne(x => x.TypeCategory)
        //    .HasForeignKey(x => x.TypeCategoryId);

    }
}