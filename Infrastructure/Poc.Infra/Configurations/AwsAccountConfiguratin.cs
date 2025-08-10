using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poc.Domain.Entities;

namespace Poc.Infra.Configurations;

public class AwsAccountConfiguration : BaseConfiguration<AwsAccount>
{
    public override void Configure(EntityTypeBuilder<AwsAccount> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Email).HasMaxLength(200);
    }
}