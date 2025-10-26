using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Poc.Infra.Configurations;

public class AuthConfigConfiguration : BaseConfiguration<AuthConfig>
{
    public override void Configure(EntityTypeBuilder<AuthConfig> builder)
    {
        base.Configure(builder);
        builder.ToTable("AuthConfigs");
        builder.HasIndex(x => x.ClientId).IsUnique();
        builder.HasIndex(x => x.ClientSecret).IsUnique();
        builder.HasIndex(x => new { x.Domain, x.Audience, x.Authority });
        builder.Property(x => x.Description).HasMaxLength(2000);
    }
}