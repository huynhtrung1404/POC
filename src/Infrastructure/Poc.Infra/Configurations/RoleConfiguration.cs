using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Poc.Infra.Configurations;

public class RoleConfiguration : BaseConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);
        builder.ToTable("Roles");
    }
}