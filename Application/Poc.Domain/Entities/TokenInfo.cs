using Poc.Domain.Core;

namespace Poc.Domain.Entities;

public class TokenInfo : BaseEntity
{
    public string? AccessToken { get; set; }
    public DateTime ExpireDate { get; set; }
    public string? TokenType { get; set; }
    public string? AwsAccessKey { get; set; }
    public string? SessionToken { get; set; }
}