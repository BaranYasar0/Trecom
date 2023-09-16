using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Api.Services.Catalog.Persistance.DataSeeding;

namespace Trecom.Api.Services.Catalog.Persistance.EntityFramework.EntityConfigurations;

public class BaseCategoryEntityConfiguration : BaseEntityConfiguration<BaseCategory>
{
    public override void Configure(EntityTypeBuilder<BaseCategory> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasColumnType("varchar(50)");
        builder.Property(x => x.Description)
            .IsRequired(false);
        builder.Property(x => x.PictureUrl)
            .HasColumnType("varchar(100)").IsRequired(false);

        //builder.HasMany(x => x.TypeCategories)
        //.WithOne(x => x.BaseCategory)
        //.HasForeignKey(x => x.BaseCategoryId);



    }
}