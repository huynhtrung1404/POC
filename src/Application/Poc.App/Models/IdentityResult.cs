namespace Poc.App.Models;

public class IdentityResult
{
    public string? AccessKeyId { get; set; }
    public string? SecretKeyId { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? SessionToken { get; set; }
}