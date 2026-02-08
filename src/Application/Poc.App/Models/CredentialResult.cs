namespace Poc.App.Models;

public class CredentialResult
{
    public string? AccessKeyId { get; set; }
    public string? SecretAccessKey { get; set; }
    public string? SessionToken { get; set; }
    public DateTime Expiration { get; set; }
    public string? AccountId { get; set; }

    public override string ToString()
        => $"AccessKey={AccessKeyId}, Expires={Expiration}";
}