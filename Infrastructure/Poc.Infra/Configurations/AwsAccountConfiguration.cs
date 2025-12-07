using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poc.Infra.Commons;

namespace Poc.Infra.Configurations;

public class AwsAccountConfiguration : BaseConfiguration<AwsAccount>
{
    public override void Configure(EntityTypeBuilder<AwsAccount> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.AccountId).HasMaxLength(20).IsEncrypted();
        builder.Property(x => x.AccountName).HasMaxLength(1000).IsEncrypted();
    }
}