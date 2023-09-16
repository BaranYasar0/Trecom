using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Persistance.EntityFramework.EntityConfigurations;

public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.IsActive)
            .ValueGeneratedOnAdd()
            .HasColumnType("bit");

        builder.Property(be => be.CreatedDate)
            .ValueGeneratedOnAdd()
            .HasColumnName("CreateDate")
            .HasColumnType("datetime");

        builder.Property(be => be.UpdatedDate)
            .ValueGeneratedOnAdd()
            .HasColumnName("ModifyDate")
            .HasColumnType("datetime");
    }
}