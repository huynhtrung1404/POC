using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Poc.Infra.Commons;

public static class EncryptionBuilderExtension
{
    public static PropertyBuilder<string?> IsEncrypted(this PropertyBuilder<string?> builder)
    {
        builder.HasAnnotation("Encrypted", true);
        return builder;
    }
}