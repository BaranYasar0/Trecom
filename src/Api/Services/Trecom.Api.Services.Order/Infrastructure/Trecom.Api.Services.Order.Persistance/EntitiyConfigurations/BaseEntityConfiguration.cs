using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Order.Persistance.EntitiyConfigurations;

public abstract class BaseEntityConfiguration<T>:IEntityTypeConfiguration<T>where T:BaseEntity
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
            .HasColumnName("CreatedDate")
            .HasColumnType("datetime");

        builder.Property(be => be.UpdatedDate)
            .IsRequired(false)
            .ValueGeneratedOnAdd()
            .HasColumnName("ModifyDate")
            .HasColumnType("datetime");
                
    }
}