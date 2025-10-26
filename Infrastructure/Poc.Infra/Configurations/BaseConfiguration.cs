using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poc.Domain.Core;

namespace Poc.Infra.Configurations;

public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.CreatedBy).HasColumnType("varchar(50)");
        builder.Property(e => e.UpdatedBy).HasColumnType("varchar(50)");
        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}