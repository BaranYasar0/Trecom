using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trecom.Api.Services.Catalog.Models.Entities;

namespace Trecom.Api.Services.Catalog.Persistance.EntityFramework.EntityConfigurations;

public class SpecificCategoryEntityConfiguration : BaseEntityConfiguration<SpecificCategory>
{
    public override void Configure(EntityTypeBuilder<SpecificCategory> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasColumnType("varchar(50)");

        builder.Property(x => x.PictureUrl)
            .HasColumnType("varchar(100)").IsRequired(false);

        

        //builder.HasOne(x => x.TypeCategory)
        //    .WithMany(x => x.SpecificCategories);


    }
}