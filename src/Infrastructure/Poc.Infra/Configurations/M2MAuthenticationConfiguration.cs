using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poc.Infra.Commons;

namespace Poc.Infra.Configurations;

public class M2MAuthenticationConfiguration : BaseConfiguration<M2MAuthentication>
{
    public override void Configure(EntityTypeBuilder<M2MAuthentication> builder)
    {
        base.Configure(builder);
        builder.HasIndex(x => new { x.ClientId, x.LoginType });
        builder.Property(x => x.ClientSecret).IsEncrypted();
    }
}