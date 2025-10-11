using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Poc.Infra.Configurations;

public class NoteConfiguration : BaseConfiguration<Note>
{
    public override void Configure(EntityTypeBuilder<Note> builder)
    {
        base.Configure(builder);
        builder.ToTable("Notes");
        builder.HasIndex(x => new { x.Title, x.Tag });
    }
}