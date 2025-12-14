using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poc.Domain.Entities;

namespace Poc.Infra.Configurations;

public class AwsOrganizationConfiguration : BaseConfiguration<AwsOrganization>
{
    public override void Configure(EntityTypeBuilder<AwsOrganization> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.OrgId).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Name).IsRequired();

    }
}