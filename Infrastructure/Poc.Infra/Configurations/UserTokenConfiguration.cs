using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Poc.Infra.Configurations;

public class UserTokenConfiguration : BaseConfiguration<Token>
{
    public override void Configure(EntityTypeBuilder<Token> builder)
    {
        base.Configure(builder);
        builder.ToTable("Tokens");
        builder.Property(x => x.RefreshToken).IsRequired();
        builder.Property(x => x.AccessToken).HasMaxLength(500).IsRequired();
    }
}