using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Poc.Infra.Configurations;

public class UserConfiguration : BaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.ToTable("Users");
        builder.Property(x => x.UserName).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(150);
        builder.Property(x => x.Password).HasMaxLength(500);
        builder.HasIndex(x => x.UserName).IsUnique();
        builder.HasIndex(x => new { x.Password, x.Email });
    }
}