using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poc.Domain.Entities;

namespace Poc.Infra.Configurations;

public class TokenConfiguration : BaseConfiguration<TokenInfo>
{
    public override void Configure(EntityTypeBuilder<TokenInfo> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.AccessToken).HasMaxLength(2000).IsRequired();
        builder.Property(x => x.TokenType).HasColumnType("varchar").HasMaxLength(32);
        builder.Property(x => x.SessionToken).HasMaxLength(2000).IsRequired();
    }
}